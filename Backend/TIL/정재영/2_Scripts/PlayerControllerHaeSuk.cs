using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerControllerHaeSuk : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField] 
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    
    private float applySpeed;



    [SerializeField]
    private float jumpForce;


    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;

    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;


    private CapsuleCollider capsuleCollider;


    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    [SerializeField]
    private Camera myCamera;

    private Rigidbody myRigid;
    private Animator animator;
    private CharacterController controller;


    float second; // Time Measurement
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider= GetComponent<CapsuleCollider>();
        myRigid= GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = myCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        IsGround();
        //TryRun();
        //Move();
        //TryJump();
        //Jump();
        if (!Inventory.inventoryActivated) 
        {
            CameraRotation();
            CharacterRotation();
        }
       

        second += Time.deltaTime;
        if (Input.GetKeyDown("space") && isGround)
        { 
            animator.SetBool("jumpFlag", true);
            animator.SetBool("walkFlag", false);
            animator.SetBool("idleFlag", false);
            Jump();
        }
        else if ((Input.GetKey("up")) || (Input.GetKey("right")) || (Input.GetKey("down")) || (Input.GetKey("left")) || Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a"))
        {
            animator.SetBool("jumpFlag", false);
            animator.SetBool("walkFlag", true);
            animator.SetBool("idleFlag", false);
            TryRun();
            Move();
        }
        else if (second >= 15)
        {
            animator.SetBool("jumpFlag", false);
            animator.SetBool("walkFlag", false);
            animator.SetBool("idleFlag", false);
            animator.SetTrigger("idleBFlag");
            second = 0;
        }
        else
        {
            animator.SetBool("jumpFlag", false);
            animator.SetBool("walkFlag", false);
            animator.SetBool("idleFlag", true);

        }




    }



    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        isCrouch = !isCrouch;

        if(isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()
    {
        float _posY = myCamera.transform.localPosition.y;
        int count = 0;

        while(_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY,applyCrouchPosY,0.3f);
            myCamera.transform.localPosition = new Vector3(0,_posY,0);
            if (count > 15)
            {
                break;
            }
            yield return null;
        }

        myCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    private void TryJump()
    {
        if(Input.GetKeyDown("space") && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isCrouch)
        {
            Crouch();
        }
       
        //animator.SetBool("jumpFlag", true);
        //animator.SetBool("walkFlag", false);
        //animator.SetBool("idleFlag", false);
        myRigid.velocity = transform.up * jumpForce;
        
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
       
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {

            RunningCancel();
        }
    }

    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void Move()
    {
        if ((Input.GetKey("up")) || (Input.GetKey("right")) || (Input.GetKey("down")) || (Input.GetKey("left")) || Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a"))
        {
            float _moveDirX = Input.GetAxis("Horizontal");
            float _moveDirZ = Input.GetAxis("Vertical");

            Vector3 _moveHorizontal = transform.right * _moveDirX;
            Vector3 _moveVertical = transform.forward * _moveDirZ;

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

            

            myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

            //animator.SetBool("jumpFlag", false);
            //animator.SetBool("walkFlag", true);
            //animator.SetBool("idleFlag", false);

        }
    }

    private void CameraRotation()
    {
        //Camera up down rotation

        float _xRotation = Input.GetAxis("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        myCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        //right left rotation
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
