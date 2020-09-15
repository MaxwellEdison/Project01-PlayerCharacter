using UnityEngine;
using System;
using System.Collections;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };
    public event Action StartJumping = delegate { };
    public event Action StartFalling = delegate { };
    public event Action StopFalling = delegate { };
    public event Action StartSprinting = delegate { };
    public AnimationClip animJump;
    public Animator charAnimator;
    public CharacterController controller;
    public Transform cam;
    public float baseSpeed = 6f;
    public float speed;
    public float sprintMulti = 1.1f;
    public float sprintSpeed
    {
        get { return baseSpeed * sprintMulti; }

    }
    public float jumpForce = 5f;
    public float gravity = -9.8f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isLanding = false;
    public bool isSprinting = false;


    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        Idle?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            CheckIfStartedJumping();
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            CheckIfStartedMoving();
            if (Input.GetButtonDown("Sprint"))
            {
                isSprinting = true;
                speed = sprintSpeed;
                this.gameObject.GetComponent<PlayerCharacterAnimator>().animRunSpd = sprintMulti;
            }
            else if(Input.GetButtonUp("Sprint"))
            {
                isSprinting = false;
                speed = baseSpeed;
                this.gameObject.GetComponent<PlayerCharacterAnimator>().animRunSpd = 1.0f;
            }
        }
        else
        {
            CheckIfStoppedMoving();
        }
        if (Mathf.Abs(direction.y) <= 0.1f && isFalling)
        {
            CheckIfStopFalling();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckIfStartedMoving()
    {

        if (isMoving == false && isSprinting == false)
        {
            StartRunning?.Invoke();
            Debug.Log("Started Running");
        }
        else if (isMoving == true && isSprinting == true)
        {
            //for some reason these animations are bugged and I can't figure out why
            StartRunning?.Invoke();
            //StartSprinting?.Invoke();
            Debug.Log("Started Sprinting");
        }
        isMoving = true;
    }
    private void CheckIfStartedJumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            StartJumping?.Invoke();
            Debug.Log("StartedJumping");
            StartCoroutine("animJumpFinish");
        }
    }

    IEnumerator animJumpFinish()
    {
        Debug.Log("started jumping coroutine");
        while (charAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;
        isJumping = false;
        CheckIfFalling();
    }

    public void CheckIfFalling()
    {
        if (!isGrounded && !isJumping)
        {
            StartFalling?.Invoke();
            Debug.Log("Started Falling");
        }
        isFalling = true;

    }
    public void CheckIfStopFalling()
    {
        if (isGrounded && isFalling)
        {
            isFalling = false;
            isLanding = true;
            StopFalling?.Invoke();
            StartCoroutine("animLandFinish");
        }

    }
    IEnumerator animLandFinish()
    {
        Debug.Log("started landing coroutine");
        while (charAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;
        isLanding = false;
        Idle?.Invoke();
        Debug.Log("stopped landing");
    }
    private void CheckIfStoppedMoving()
    {
        if (isMoving == true)
        {
            Idle?.Invoke();
            Debug.Log("Stopped Running");
        }
        isMoving = false;
    }
}
