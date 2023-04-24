 using UnityEngine;
using Cinemachine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 20.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 40.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

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
        public Animator nowAnim;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        // item management
        GameObject nearObject;
        GameObject equipWeapon;
        GameObject equipArrow;
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
        public float aimDistance = 1f;
        public float maxDistance = 1000f; // 레이캐스트가 탐색할 최대 거리

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

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


        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            nowAnim = GetComponent<Animator>();
            right_WeaponSocket = GameObject.Find("R_WeaponSocket");

#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            
            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();
            Attack();
            InterAction();
            Swap();
            AimShoot();
        }

        private void AimShoot()
        {
            
            if (_input.IsAiming && Grounded && !_input.sprint)
            {
                //Play Aim Animation
                _animator.SetBool("IsAiming", _input.IsAiming);
                _animator.SetBool("Shooting", _input.IsShooting);
                playerFollowCamera.SetActive(false);
                playerAimCamera.SetActive(true);
                // 조준 카메라 찾는 곳
                aimCamera = GameObject.Find("PlayerAimCamera").GetComponent<CinemachineVirtualCamera>();
                Debug.DrawLine(aimCamera.transform.position, aimCamera.transform.forward * 150f, Color.red);
            }
            else
            {
                //Stop Aim Animation
                _animator.SetBool("IsAiming", false);
                _animator.SetBool("Shooting", false);
                playerFollowCamera.SetActive(true);
                playerAimCamera.SetActive(false);
            }
        }
        //ArrowFire
            //DrawAnim
        public void ArrowSocket_Draw(bool IsDraw)
        {
            if (IsDraw)
            {
                right_WeaponSocket.transform.localPosition = new Vector3(-0.334f, 0.024f, -0.071f);
                right_WeaponSocket.transform.localRotation = Quaternion.Euler(0.061f, -111.517f, -0.024f);
            }
            else
            {
                right_WeaponSocket.transform.localPosition = new Vector3(-0.094f, 0.0313f, 0f);
                right_WeaponSocket.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        public void AnimEvent_DrawArrow()
        {
            ArrowSocket_Draw(true);
        }
            //Shoot
        public LayerMask ArrowDetectionLayer;
        public void Shoot()
        {
            RaycastHit hitInfo; // 레이캐스트에서 반환된 정보
            if (Physics.Raycast(aimCamera.transform.position, aimCamera.transform.forward, out hitInfo, maxDistance, ArrowDetectionLayer))
            {
                aimTarget.position = hitInfo.point;
            }
            else
            {
                aimTarget.position = aimCamera.transform.forward * 100f;
            }
           
            Vector3 fireDirection = (aimTarget.position - arrowPoint.position).normalized;

            GameObject arrow = Instantiate(arrowObject, arrowPoint.position + transform.forward, Quaternion.LookRotation(fireDirection));
            arrow.GetComponent<Rigidbody>().AddForce(-fireDirection * 150f, ForceMode.VelocityChange);

            //ArrowSocket Back
            ArrowSocket_Draw(false);
        }


        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDAttack = Animator.StringToHash("IsAttack");
        }

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
        }

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

        private void Move()
        {
            // 공격중이면 못 움직임
            IsAttack = false;

            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

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
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


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
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
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

                // if we are not grounded, do not jump
                _input.jump = false;
            }
            

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private void Attack()
        {

            //Atack
            if (_input.attack)
            {
                IsAttack = true;

                if (_hasAnimator)
                {
                    _animator.SetTrigger(_animIDAttack);
                }


            }

            _input.attack = false;

        }

        private void InterAction()
        {
            if(_input.itemGain && nearObject != null && Grounded)
            {
                if(nearObject.tag == "Weapon")
                {
                    Item item = nearObject.GetComponent<Item>();
                    // 인덱스가 0이면 활, 2이면 근접.
                    int weaponIndex = item.value;
                    hasWeapons[weaponIndex] = true;

                    Destroy(nearObject);
                }
            }
            _input.itemGain = false;
        }


        // 1번과 2번만 작동한다.
        private void Swap()
        {

            int weaponIndex = -1;
           
            if (_input.bowNArrow)
            {
                //Debug.Log("활");
                weaponIndex = 0;
            }
            if (_input.axe)
            {
                //Debug.Log("도끼");
                weaponIndex = 1;
            }

            if (_input.item3)
            {
                weaponIndex = 2;

                // 아직 구현 안함
                
            }


            //if ((_input.bowNArrow || _input.axe || _input.item3) && Grounded)
            if((_input.bowNArrow || _input.axe) && Grounded)
            {
           
                // 활과 화살을 장착하고 있는 경우
                if(equipWeapon != null && equipArrow != null)
                {
                    equipWeapon.SetActive(false);
                    equipArrow.SetActive(false);

                    if (_input.bowNArrow)
                    {
                        nowAnim.runtimeAnimatorController = noneAnimController;
                        equipWeapon = null;
                        equipArrow = null;
                        
                    }
                    else if(_input.axe)
                    {
                        nowAnim.runtimeAnimatorController = axeAnimController;
                        equipWeapon = weapons[weaponIndex + 1];
                        equipWeapon.SetActive(true);

                        // 화살만 null로 해준다.
                        equipArrow = null;

                    }

                // 도끼를 장착하고 있는 경우
                }else if(equipWeapon != null)
                {
                    //Debug.Log("도끼장착해제: " + equipWeapon);
                    equipWeapon.SetActive(false);
                    if (_input.axe)
                    {
                        nowAnim.runtimeAnimatorController = noneAnimController;
                        equipWeapon = null;

                    }
                    else if(_input.bowNArrow)
                    {
                        nowAnim.runtimeAnimatorController = bowAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipArrow = weapons[weaponIndex + 1];
                        equipWeapon.SetActive(true);
                        equipArrow.SetActive(true);

                    }

                // 무기를 장착하고 있지 않은 경우
                }else if(equipWeapon == null && equipArrow == null)
                {
                    //Debug.Log("무기가 없어요");
                    if (weaponIndex == 0)
                    {
                        //Debug.Log("무기가 없으면 활을 쓰자");
                        nowAnim.runtimeAnimatorController = bowAnimController;
                        equipWeapon = weapons[weaponIndex];
                        equipArrow = weapons[weaponIndex + 1];
                        weapons[weaponIndex].SetActive(true);
                        weapons[weaponIndex + 1].SetActive(true);
                    }
                    else if (weaponIndex == 1)
                    {
                        //Debug.Log("무기가 없으면 도끼를 쓰자");
                        nowAnim.runtimeAnimatorController = axeAnimController;
                        equipWeapon = weapons[weaponIndex + 1];
                        weapons[weaponIndex + 1].SetActive(true);
                    }
                }
                

            }

            _input.bowNArrow = false;
            _input.axe = false;
            _input.item3 = false;

        }

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

        private void OnTriggerStay(Collider other)
        {
            if(other.tag == "Weapon")
            {
                Debug.Log("weapon");
                nearObject = other.gameObject;
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag == "Weapon")
            {
                nearObject = null;
            }
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        public bool GetSprint()
        {
            return _input.sprint;
        }
    }
}