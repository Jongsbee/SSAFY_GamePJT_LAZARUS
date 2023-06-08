
using UnityEngine;
using Cinemachine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif
using UnityEngine.SceneManagement;
/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonControllerForSurvive : MonoBehaviour
    {
        public static bool uiClosed = true;
        public static bool cannotMove = false;

        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 80.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 200.0f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float turnSmoothTime = 0.12f;

        public float speedSmoothTime = 0.12f;

        private float turnSmoothVelocity;
        private float speedSmoothVelocity;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.8f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Header("Player Attack")]
        [Tooltip("If the character is attacking or not. Not part of the CharacterController built in attack check")]
        public bool IsAttack = false;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDAttack;

        // anim Controller
        public RuntimeAnimatorController bowAnimController;
        public RuntimeAnimatorController axeAnimController;
        public RuntimeAnimatorController noneAnimController;
        public RuntimeAnimatorController RifleAnimController;
        public RuntimeAnimatorController PickAxeAnimController;
        public Animator nowAnim;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private Camera _mainCamera;

        // item management
        GameObject nearObject;
        public GameObject equipWeapon;
        public GameObject equipArrow;
        public GameObject playerFollowCamera;
        public GameObject playerAimCamera;
        public GameObject[] weapons;
        public bool[] hasWeapons;


        // arrow control
        GameObject right_WeaponSocket;
        public GameObject arrowObject;
        public Transform arrowPoint;
        public Transform aimTarget;
        private CinemachineVirtualCamera aimCamera;
        private GameObject crossHair;
        public float aimDistance = 1f;
        public float maxDistance = 10f; // ?�이캐스?��? ?�색??최�? 거리

        private const float _threshold = 0.01f;

        // ?�니메이??rigidbody�??�한 positon rotation 컨트�?
        // => ?�니??clip ?�서 ?�결가??
        // ?�비 ?�니메이??처리
        Equipments equipments;
        public int equipItemIndex = -1; // ?�재 ?�착중인 ?�이?�의 ?�덱??// ?�일 경우 ?�덱?�는 -1 ?�다.
 

        // 카메??처리
        private bool _hasAnimator;

        // 모든 ?�력�?처리
        private bool[] inputList;

        // 캐릭???�프 ?�태
        private bool isJump;

        // 무기 ?�과???�생???�한 Manager
        private SFXManager _sFXManager;

        ///////////// �?관??변?�들
        public enum AimState
        {
            Idle,
            Aiming
        }

        public AimState aimState { get; private set; }

        public AssaultRifle assaultRifle; // ?�용??�?
        public LayerMask excludeTarget; // ���� ĳ��Ʈ�� ������ ���� ���̾� ����

        private float waitingTimeForReleasingAim = 2.5f;
        private float lastFireInputTime;
        // 총이 발사???�치
        private Vector3 aimPoint;
        //Depricated
        private bool linedUp => !(Mathf.Abs(_mainCamera.transform.eulerAngles.y - transform.eulerAngles.y) > 1f);
        //Depricated
        private bool hasEnoughDistance => !Physics.Linecast(transform.position + Vector3.up * assaultRifle.fireTransform.position.y, assaultRifle.fireTransform.position, ~excludeTarget);

        /////// ?�로?�헤??변?�들

        public GameObject bowCrosshair;
        public GameObject gunCrosshair;
        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
                return false;
#endif
            }
        }


        // Main Camera 게임 ?�브?�트 가?�오�?
        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }

            // excludeTarget???�레?�어??캐릭???�브?�트???�이?��? ?�함?�어 ?��? ?�다�??�레?�어 게임 ?�브?�트???�이?��?
            // excludeTarget??추�??�는 코드?�다.
            // ?�레?�어가 ?�수�??�레?�어 ?�신???�는 ?�이 ?�어?��? ?�도�?미리 ?�외처리�???것이??
            if (excludeTarget != (excludeTarget | (1 << gameObject.layer)))
            {
                excludeTarget |= 1 << gameObject.layer;
            }

            //SFXManager 불러?�기
            _sFXManager = GameObject.FindObjectOfType<SFXManager>();
        }

        //
        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            nowAnim = GetComponent<Animator>();
            right_WeaponSocket = GameObject.Find("R_WeaponSocket");
            crossHair = GameObject.Find("AimTarget");
            crossHair.SetActive(false);

            //TODO : ?�재??처음???�만 ?�성???�어 ?�기 ?�문???�만 가?? ?�른 무기??가?�하�??�정?�야 ??
            equipments = GetComponentInChildren<Equipments>();

#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
            Debug.LogError("Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        bool check;

        private void Update()
        {
            // ���� ���� ���ξ��� ���� �۵��ϵ��� �Ѵ�.
            // TODO: �� ���� �� ĳ���Ϳ� �����Ǿ� �ִ� ��ũ���� ���� ��� ���� �ٸ� ��� �����ϱ�. ������Ʈ���� �Ź� �̷������� üũ�ϴ� ���� ��ȿ�����̴�.
            if (SceneManager.GetActiveScene().name == "JaeYoung_Lobby" || SceneManager.GetActiveScene().name == "HaeSuk_Lobby") return;
            else
            {
                if (!check)
                {
                    //TODO: ĳ������ ��ġ�� �ٲ��� �ʴ´�. �ٸ� ����� ã�ƾ� �Ѵ�.
                    Vector3 desiredPosition = new Vector3(372f, 20f, 256.7f);
                    Quaternion desiredRotation = Quaternion.Euler(0f, 172f, 0f);
        
                    // ?�역 공간?�서 로컬 공간?�로 변?�하???�용
                    transform.localPosition = desiredPosition;
                    transform.localRotation = desiredRotation;

                    //SFXManager 불러?�기
                    _sFXManager = GameObject.FindObjectOfType<SFXManager>();
                    equipments = GetComponentInChildren<Equipments>();
                    check = true;
                }

            }
            // ĳ���Ͱ� �׾��ٸ� �Ʒ� �Է� ������ ���̻� ���� �ʴ´�.
            // TODO: ���� ���¿��� �Է¹��� ������ �ʱ�ȭ ������� �Ѵ�.
            if (StatusController.instance.isDead) return;


            _hasAnimator = TryGetComponent(out _animator);
           
            // 캐릭?��? ?�러�?경우 ?�어???�까지 ?�력??받�? ?�는??
            // 공격??받�? ?�는??- 종섭 추�?
            if (cannotMove)
            {
                // ����Ű�� �Է¹��� ��� �Ͼ�� �ִϸ��̼� ���
                if (_input.move != Vector2.zero)
                {
                    _animator.SetTrigger("IsMove");
                    _input.move = Vector2.zero;
                }
                // ?�른 ?�력값�? ?��? false�?바꿔준??
                _input.attack = _input.jump = _input.sprint = _input.itemGain = _input.bowNArrow = _input.axe
                    = _input.assaultGun = _input.IsAiming = _input.IsShooting = false;
                return;
            }

            JumpAndGravity();
            GroundedCheck();
            Move();
            if (uiClosed)

            // UI â�� ������ ���, ���콺 ������ �Ǵ� ������ ���� ��� ī�޶� ȸ����Ų��, ���������� �Ұ����ϴ�.
            // TODO: 
            {
                if (_input.IsShooting || aimState == AimState.Aiming) RotateCamera();
            MeleeAttack();
            }
            InterAction();
            // ���� ���� ���� ���� ���� �Ұ�
            if (!_input.IsAiming)
            {
                Swap();
            }
            
            SwapInputReset();

            if (uiClosed)
            {
                AimShoot();

                if (equipItemIndex == 3 && _input.IsShooting)
                {
                    lastFireInputTime = Time.time;
                    GunShoot();
                    _input.IsShooting = false;
        }
                else if (equipItemIndex == 3 && _input.IsReload)
                {
                    GunReload();
                _input.IsReload = false;
            }

        }

        }

        // ȭ��� ���� �����ϴ� �ִϸ��̼ǰ� ī�޶� Ȱ��ȭ.
        private void AimShoot()
        {
            if(equipItemIndex != 2 && equipItemIndex != 3)
            {
                return;
            }

            if(!_input.IsShooting && Time.time >= lastFireInputTime + waitingTimeForReleasingAim)
            {
                aimState = AimState.Idle;
            }
           
            // ���� ���̰� ���̰� �޸��� ���� ���� ���� ����
            if (_input.IsAiming && Grounded && !_input.sprint)
            {
                UpdateAimTarget();
                //Play Aim Animation
                _animator.SetBool("IsAiming", _input.IsAiming);
                playerFollowCamera.SetActive(false);
                playerAimCamera.SetActive(true);
                //_sFXManager.WeaponDrawSFX(equipItemIndex); // �ܳ� ȿ���� ���
                // Ȱ�� ���
                if (equipItemIndex == 2)
                {
                    _animator.SetBool("Shooting", _input.IsShooting);          
                    crossHair.SetActive(true);
                    bowCrosshair.SetActive(true);

                }// ���� ���
                else if(equipItemIndex == 3)
                {
                    _animator.SetBool("Shooting", _input.IsShooting);
                    crossHair.SetActive(true);
                    gunCrosshair.SetActive(true);
                    
                }
               
                //crossHair.transform.GetChild(0).transform.GetChild(equipItemIndex).gameObject.SetActive(true);

                // ���� ī�޶� ã�� ��
                //aimCamera = GameObject.Find("PlayerAimCamera").GetComponent<CinemachineVirtualCamera>();
                //Debug.DrawLine(aimCamera.transform.position, aimCamera.transform.forward * 150f, Color.red);
            }
            else
            {
                //Stop Aim Animation
                _animator.SetBool("IsAiming", false);
                _animator.SetBool("Shooting", false);
                playerFollowCamera.SetActive(true);
                playerAimCamera.SetActive(false);
                bowCrosshair.SetActive(false);
                gunCrosshair.SetActive(false);
                crossHair.SetActive(false); 
            }

        }
        /////////////////////////////////////////////// ?�살???�는 로직�??�니메이??
        public void ArrowSocket_Draw(bool IsDraw)
        {
            if (IsDraw)
            {
                right_WeaponSocket.transform.localPosition = new Vector3(0.388f, 0.295f, 0.142f);
                right_WeaponSocket.transform.localRotation = Quaternion.Euler(29.58f, 4.671f, 31.114f);
            }
            else
            {
                right_WeaponSocket.transform.localPosition = new Vector3(0.06103601f, -0.02312678f, 0.06130019f);
                right_WeaponSocket.transform.localRotation = Quaternion.Euler(9.098f, 72.005f, 10.74f);
            }
        }

        // 걸어?�니면서 조�??????�살???�치,?�전�?변�?
        public void ArrowSocket_Draw_Walking(bool IsDraw)
        {
            if (IsDraw)
            {
                right_WeaponSocket.transform.localPosition = new Vector3(0.4322741f, -0.08826452f, 0.1344467f);
                right_WeaponSocket.transform.localRotation = Quaternion.Euler(80.885f, 248.579f, 242.677f);
            }
            else
            {
                right_WeaponSocket.transform.localPosition = new Vector3(0.06103601f, -0.02312678f, 0.06130019f);
                right_WeaponSocket.transform.localRotation = Quaternion.Euler(9.098f, 72.005f, 10.74f);
            }
        }
        public void AnimEvent_DrawArrow()
        {
            ArrowSocket_Draw(true);
        }

        public void AnimEvent_DrawArrow_Walking()
        {
            ArrowSocket_Draw_Walking(true);
        }

        public void AnimEvent_StopDrawArrow()
        {
            ArrowSocket_Draw(false);
        }
        //Shoot
        public LayerMask ArrowDetectionLayer;
        public void ShootArrow()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            aimTarget.position = transform.position + ray.direction * 100f;

            Vector3 fireDirection = (aimTarget.position - arrowPoint.position).normalized;
            //crossHairAnim.FireAnimation();
            Debug.Log("ȭ��߽�");
            //GameObject arrow = Instantiate(arrowObject, arrowPoint.position + transform.forward, Quaternion.LookRotation(fireDirection));
            GameObject arrow = Instantiate(arrowObject, arrowPoint.position, Quaternion.LookRotation(fireDirection));
            arrow.GetComponent<Arrow>().SetUp(transform.position); // ������ �÷��̾� ��ġ�� �����ϱ� ���� �߰� (����)
            arrow.GetComponent<Rigidbody>().AddForce(fireDirection * 200f, ForceMode.Impulse);

            _sFXManager.WeaponUsingSFX(equipItemIndex); //��� ȿ���� ���

            Inventory.Instance.arrowRemain -= 1;
            UIManager.instance.arrowCntText.text = Inventory.Instance.arrowRemain.ToString();

            //ArrowSocket Back
            ArrowSocket_Draw(false);


            // ������ false �� �ٲ۴�.
            IsAttack = false;
            _input.attack = false;
        }
        ///////////////////// ?�살???�는 로직�??�니메이??---------- ?�기까�? ??////////////

        ////////////////////// 총을 ?�는 로직�??�니메이??////////////////////
        public void GunShoot()
        {
            if(aimState == AimState.Idle)
            {
             aimState = AimState.Aiming;
            }else if(aimState == AimState.Aiming)
            {
                if (assaultRifle.Fire(aimPoint))
                {
                    _animator.SetTrigger("Shoot");
                }
            }

        }

        public void GunReload()
        {
            if (assaultRifle.Reload())
            {
                _animator.SetTrigger("Reload");
            }
        }

        private void UpdateAimTarget()
        {
            if (!_input.IsAiming) return;
            RaycastHit hit;

            // ī�޶� ���߾ӿ��� ������ �߻�
            var ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            // tps ���ӿ��� ���� �ѱ����� �������� ������ �������� ���� ��ü�� ��ȯ
            if (Physics.Raycast(ray, out hit, assaultRifle.fireDistance, ~excludeTarget))
            {
                aimPoint = hit.point;

                if(Physics.Linecast(assaultRifle.fireTransform.position,hit.point, out hit, ~excludeTarget))
                {
                    aimPoint = hit.point;
                }

            }// ����ĳ��Ʈ�� �ɸ� ��ü�� ���ٸ� ī�޶� ���߾ӿ��� ���� ĳ��Ʈ �Ÿ���ŭ ���� Ÿ���� �ȴ�.
            else
            {
                aimPoint = _mainCamera.transform.position + _mainCamera.transform.forward * assaultRifle.fireDistance;
            }
        }


 
        //    // IK?? ?????? ????? ????? ????? ???? ?????? ??????? ?????
        //    _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        //    _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);


        private void FixedUpdate()
        {


                }

        private void LateUpdate()
        {
            
            if (SceneManager.GetActiveScene().name == "JaeYoung_Lobby" || SceneManager.GetActiveScene().name == "HaeSuk_Lobby") return;

            if (uiClosed)
            {
                CameraRotation();
            }
            
        }

        // ?�니메이???�라미터 리스??
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDAttack = Animator.StringToHash("IsAttack");
        }

        //케�?��가 ?�에 ?�았?��? 체크
        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
                
            }

            if (Grounded)
            {
                isJump = false; // ?�이�??�프중이 ?�님?�로 초기??
            }
        }

        // 마우???�직임???�한 카메???�전 구현
        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 3.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        // �÷��̾� ĳ���� ȸ���ϱ�
        private void RotateCamera()
        {
            var targetRotation = playerFollowCamera.transform.eulerAngles.y;

            targetRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref 
                turnSmoothVelocity, turnSmoothTime);

            transform.eulerAngles = Vector3.up * targetRotation;
        }

        // ?�동 구현
        private void Move()
        {
            //공격중이�?�??�직임
            if (IsAttack || _input.attack)
            {
                _input.move = Vector2.zero;
                return;
            }


            // set target speed based on move speed, sprint speed and if sprint is pressed

            float targetSpeed;
            float sp = StatusController.instance.GetCurrentSP();
            if (_input.sprint && sp > 1)
            {
                StatusController.instance.DecreaseStamina(0.2f);
                targetSpeed = SprintSpeed;
            }else
            {
                targetSpeed = MoveSpeed;
            }

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;
           

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    turnSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                
            }
            else
                _animator.ResetTrigger("IsMove");


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }

            

        }

        private void JumpAndGravity()
        {

            if (Grounded)
            {
                isJump = false;
              
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    if (StatusController.instance.DecreaseStamina(10)) // ����Ű�� ������ ���� Ű�� ��ȿ
                    {
                        // ����Ű�� ������ ���� Ű�� ��ȿ
                        if (_input.attack)
                        {
                            _input.attack = false;
                            IsAttack = false;
                        }
                        isJump = true;
                        // the square root of H * -2 * G = how much velocity needed to reach desired height
                        _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                        if (_hasAnimator)
                        {
                            _animator.SetBool(_animIDJump, true);

                        }
                    }
                    
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // �߶����̸� ����Ű ��ȿ
                if (_input.attack)
                {
                    _input.attack = false;
                    IsAttack = false;
                }

                // ���߿��� �� �� ������ �Ұ�
                if (_input.jump)
                {
                    _input.jump = false;
                }

                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;
                
                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                
            }

            _input.jump = false;

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        // 근접 ?�비 ?�리�?
        private void MeleeAttack()
        {

            
            // ?�프 ?�력?� 무시
            if (_input.jump )
            {
                _input.jump = false;
               
            }
            // ?�프중이�?리턴
            if (isJump)
            {
                return;
            }

            if (equipItemIndex == 2)
            {
                _input.attack = false;
                return;
            }

            if (equipItemIndex == 3)
            {
                _input.attack = false;
                return;
            }


            //Atack
            if (_input.attack)
            {
                IsAttack = true;

                if (_hasAnimator)
                {
                    _animator.SetTrigger(_animIDAttack);
                }
                //_sFXManager.WeaponUsingSFX(equipItemIndex); //?�비 ?�과???�생

            }
          

            _input.attack = false;

        }

        private void InterAction()
        {
            if (_input.itemGain && nearObject != null && Grounded)
            {
                if (nearObject.tag == "Weapon")
                {
                    Item item = nearObject.GetComponent<Item>();
                    // ?�덱?��? 0?�면 ?? 2?�면 근접.
                    //int weaponIndex = item.value;
                    //hasWeapons[weaponIndex] = true;

                    Destroy(nearObject);
                }
            }
            _input.itemGain = false;
        }


        // ��� �ٲٱ� �ڵ�
        private void Swap()
        {
            // �������̸� ���� �Ұ���, ���̸� ���� �Ұ���
            if (IsAttack || _input.attack || !Grounded)
            {
                _input.attack = false;
                return;
            }

            

            int weaponIndex = -1;

            if (_input.pickAxe)
            {
                Debug.Log("��� Ű ����");
                // ��� �������� ������ ����
                //if (ItemEnums.DamageTable[1] == 0) return;
                //���
                weaponIndex = 0;
                UIManager.instance.changeEquip(1);

            }
            if (_input.axe)
            {
                // ��� �������� ������ ����
                        
                //if (ItemEnums.DamageTable[2] == 0) return;
                // ����
                weaponIndex = 1;
                UIManager.instance.changeEquip(2);

            }

            if (_input.bowNArrow)
            {
                // ��� �������� ������ ����
                //if (ItemEnums.DamageTable[3] == 0) return;
                // Ȱ
                weaponIndex = 2;
                UIManager.instance.changeEquip(3);

            }

            if (_input.assaultGun)
            {
                // ��� �������� ������ ����
                //if (ItemEnums.DamageTable[4] == 0) return;
                // ��
                weaponIndex = 3;
                UIManager.instance.changeEquip(4);
            }

            if (_input.bowNArrow || _input.axe || _input.assaultGun || _input.pickAxe)
            {

                // Ȱ�� ȭ���� �����ϰ� �ִ� ���
                if (equipItemIndex == 2)
                {
                    equipWeapon.SetActive(false);
                    equipArrow.SetActive(false);
                    equipArrow = null;
                    if (_input.bowNArrow)
                    {
                        nowAnim.runtimeAnimatorController = noneAnimController;
                        equipWeapon = null;
                        equipItemIndex = -1;
                    }
                    else if (_input.axe)
                    {
                        nowAnim.runtimeAnimatorController = axeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipWeapon.SetActive(true);
                        equipItemIndex = 1;
                        // ?�살�?null�??��???


                    } else if (_input.assaultGun)
                    {
                        nowAnim.runtimeAnimatorController = RifleAnimController;
                        equipWeapon = weapons[weaponIndex + 1];
                        equipWeapon.SetActive(true);
                        assaultRifle.Setup(this);
                        aimState = AimState.Idle;
                        equipItemIndex = 3;
                    } else if (_input.pickAxe)
                    {
                        nowAnim.runtimeAnimatorController = PickAxeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipWeapon.SetActive(true);
                        equipItemIndex = 0;

                    }

                    // ������ �����ϰ� �ִ� ���
                }
                else if (equipItemIndex == 1)
                {
                    //Debug.Log("������������: " + equipWeapon);
                    equipWeapon.SetActive(false);
                    UIManager.instance.changeEquip(0);
                    if (_input.axe)
                    {
                        nowAnim.runtimeAnimatorController = noneAnimController;
                        equipWeapon = null;
                        equipItemIndex = -1;
                    }
                    else if (_input.bowNArrow)
                    {
                        nowAnim.runtimeAnimatorController = bowAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipArrow = weapons[weaponIndex + 1];
                        equipWeapon.SetActive(true);
                        equipArrow.SetActive(true);
                        equipItemIndex = 2;
                    }
                    else if (_input.assaultGun)
                    {
                        nowAnim.runtimeAnimatorController = RifleAnimController;
                        equipWeapon = weapons[weaponIndex + 1];
                        weapons[weaponIndex + 1].SetActive(true);
                        assaultRifle.Setup(this);
                        aimState = AimState.Idle;
                        equipItemIndex = 3;
                    } else if (_input.pickAxe)
                    {
                        nowAnim.runtimeAnimatorController = PickAxeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        weapons[weaponIndex].SetActive(true);
                        equipItemIndex = 0;
                    }

                    // ���� �����ϰ� �ִ� ���
                }
                else if (equipItemIndex == 3)
                {
                    equipWeapon.SetActive(false);
                    if (_input.assaultGun)
                    {
                        nowAnim.runtimeAnimatorController = noneAnimController;
                        aimState = AimState.Idle;
                        equipWeapon = null;
                        equipItemIndex = -1;
                    }
                    else if (_input.bowNArrow)
                    {
                        nowAnim.runtimeAnimatorController = bowAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipArrow = weapons[weaponIndex + 1];
                        equipWeapon.SetActive(true);
                        equipArrow.SetActive(true);
                        equipItemIndex = 2;
                    }
                    else if (_input.axe)
                    {
                        nowAnim.runtimeAnimatorController = axeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        weapons[weaponIndex].SetActive(true);
                        equipItemIndex = 1;

                    } else if (_input.pickAxe)
                    {
                        nowAnim.runtimeAnimatorController = PickAxeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        weapons[weaponIndex].SetActive(true);
                        equipItemIndex = 0;
                    }
                } else if(equipItemIndex == 0){

                    equipWeapon.SetActive(false);
                    if (_input.pickAxe)
                    {
                        nowAnim.runtimeAnimatorController = noneAnimController;
                        equipWeapon = null;
                        equipItemIndex = -1;
                    }
                    else if (_input.bowNArrow)
                    {
                        nowAnim.runtimeAnimatorController = bowAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipArrow = weapons[weaponIndex + 1];
                        equipWeapon.SetActive(true);
                        equipArrow.SetActive(true);
                        equipItemIndex = 2;
                    }
                    else if (_input.axe)
                    {
                        nowAnim.runtimeAnimatorController = axeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        weapons[weaponIndex].SetActive(true);
                        equipItemIndex = 1;

                    }
                    else if (_input.assaultGun)
                    {
                        nowAnim.runtimeAnimatorController = RifleAnimController;
                        equipWeapon = weapons[weaponIndex + 1];
                        weapons[weaponIndex + 1].SetActive(true);
                        assaultRifle.Setup(this);
                        aimState = AimState.Idle;
                        equipItemIndex = 3;
                    }

                    // ���⸦ �����ϰ� ���� ���� ���
                }
                else if (equipItemIndex == -1)
                {
                    Debug.Log("���Ⱑ �����");
                    if (weaponIndex == 2)
                    {
                        //Debug.Log("���Ⱑ ������ Ȱ�� ����");
                        nowAnim.runtimeAnimatorController = bowAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipArrow = weapons[weaponIndex + 1];
                        weapons[weaponIndex].SetActive(true);
                        weapons[weaponIndex + 1].SetActive(true);
                        equipItemIndex = 2;
                    }
                    else if (weaponIndex == 1)
                    {
                        //Debug.Log("���Ⱑ ������ ������ ����");
                        nowAnim.runtimeAnimatorController = axeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        weapons[weaponIndex].SetActive(true);
                        equipItemIndex = 1;
                    }
                    else if (weaponIndex == 3)
                    {
                        aimState = AimState.Idle;
                        nowAnim.runtimeAnimatorController = RifleAnimController;
                        equipWeapon = weapons[weaponIndex + 1];
                        weapons[weaponIndex + 1].SetActive(true);
                        assaultRifle.Setup(this);
                        equipItemIndex = 3;

                    }else if(weaponIndex == 0)
                    {
                        nowAnim.runtimeAnimatorController = PickAxeAnimController;
                        equipWeapon = weapons[weaponIndex];
                        weapons[weaponIndex].SetActive(true);
                        equipItemIndex = 0;

                    }
                }

                
                
            }

            _input.bowNArrow = false;
            _input.axe = false;
            _input.assaultGun = false;
            _input.pickAxe = false;

        }
        
        // ������ �Ұ����� ���¿��� �Էµ� ����Ű �����ϱ�
        private void SwapInputReset()
        {
            _input.axe = _input.bowNArrow = _input.assaultGun = _input.itemGain = _input.pickAxe = false;
        }
        
        //���� 360 �� ���Ϸ� ���߱�
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        // ������, �̺�Ʈ ���� ǥ���ϱ�
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Weapon")
            {
                Debug.Log("weapon");
                nearObject = other.gameObject;
            }
            else if(other.tag.Equals("Item"))
            {
                Item item= other.GetComponent<Item>();
                ActionController.instance.ItemInfoAppear(item);
            }
            else if (other.tag == "Event")
            {
                EventController.instance.EventInfoAppear(other.gameObject);
            }


        }

        // �˸�â ���ֱ�
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Weapon")
            {
                nearObject = null;
            }

            ActionController.instance.ItemInfoDisappear();
            EventController.instance.eventInfoDisappear();
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            //�ӵ��� 1���� ũ�� �߼Ҹ��� ����.
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        // ���� ����� �� �Ҹ����� �ϱ�
        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }
        
        //������Ʈ �����̸� true
        public bool GetSprint()
        {
            return _input.sprint;
        }

        // 캐릭???�어?�기 ?�니메이??처리
        public void Standing_Anim_ConditionControl()
        {
            //StatusController.instance.isDown = false;
            cannotMove = false;
            // �??�안 ?�력받�? ?�직임?� ??무시?�다.
            
        }

        // 캐릭???�러�??�니메이??처리
        public void Down_Anim_ConditionControl()
        {
            cannotMove = true;
            // 5초동?��? 무적?�다.
            StartCoroutine(StatusController.instance.cannotAttackForSeconds(5f));
            //StatusController.instance.isDown = true;
        }

        // ?�비 ?�니메이??콜라?�더 처리 �??�니메이??종료???�태 처리

        // 주먹
        public void Punch_Start()
        {
            IsAttack = true;
            equipments.Punch_Start_Animation();
        }

        public void Punch_End()
        {
            equipments.Punch_End_Animation();
            _animator.ResetTrigger(_animIDAttack);
            IsAttack = false;

        }

        // ?�끼
        public void Axe_Start()
        {
            IsAttack = true;
            equipments.Axe_Start_Animation();
        }

        public void Axe_End()
        {
            equipments.Axe_End_Animation();
            _animator.ResetTrigger(_animIDAttack);
            IsAttack = false;
        }

        public void IsAttack_Reset()
        {
            right_WeaponSocket.transform.localPosition = new Vector3(0.06103601f, -0.02312678f, 0.06130019f);
            right_WeaponSocket.transform.localRotation = Quaternion.Euler(9.098f, 72.005f, 10.74f);

            IsAttack = false;
        }

        public void Reload_Start()
        {
            IsAttack = true;
        }

        public void Reload_End()
        {
            IsAttack = false;
        }

        public void Gun_Start()
        {
            //IsAttack = true;

        }

        public void Gun_End()
        {
            IsAttack = false;
        }


    }
}