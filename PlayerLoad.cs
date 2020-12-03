using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed=2;
    public float runSpeed=6;
    public float turnSmoothTime=0.001f;
    public float gravity=-2f;
    public float jumpHeight=3f;
    public float speedSmoothTime = 0.1f;
    public float controlTime = 1.1f;
    public static int lives = 3;

    float velocityY;
    float jumpcount = 0f;
    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed;
    Animator animator;
    CharacterController characterController;
    bool jumping=false;
    bool jumpStart=false;
    float timeFromJumpStart = 0f;
    float timeToFall=0f;
    Transform cameraT;
    Vector2 inputdir;
    bool walking;
    bool disabled = false;
    Vector3 vel;

    void Start()
    {
        animator=GetComponent<Animator>();
        characterController=GetComponent<CharacterController>(); 
        cameraT=Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {


        horizontalInput();
        verticalInput();
        calculateVelocity();
        animate();
      
    }

    void horizontalInput()
    {
        inputdir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (disabled)
            inputdir = new Vector2(0, 0);
        inputdir = inputdir.normalized;
        if (inputdir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputdir.x, inputdir.y) * Mathf.Rad2Deg+cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        walking = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (Victory.firstTime)
            walking = true;
        float targetSpeed = ((walking) ? walkSpeed : runSpeed) * inputdir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
    }

    void verticalInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && jumpcount < 2f)
        {
            transform.parent = null;
            Jump();
        }
        if (jumpStart)
            timeFromJumpStart += Time.deltaTime;//this part is for the second jump animation to look natural
        if (timeFromJumpStart > 0.04f)
        {
            jumpStart = false;
            timeFromJumpStart = 0;
        }

    }

    void Jump()
    {
        if (!disabled)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            jumpStart = true;
            jumpcount++;
            jumping = true;
        }
    }

    void calculateVelocity()
    {
        velocityY += Time.deltaTime * gravity;
        vel = transform.forward * currentSpeed + Vector3.up * velocityY;
        characterController.Move(vel * Time.deltaTime);
        currentSpeed = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

        if (!characterController.isGrounded)
        {
            if (!jumping)//if character controller is off the ground and falling
            {
                jumpcount = 1;
                jumping = true;
            }
        }
        else
        {
            velocityY = 0;
            jumpcount = 0f;
            jumping = false;
        }

    }

   

    void animate()
    {

        float animationSpeedPercent = ((walking) ? currentSpeed / walkSpeed * .25f : currentSpeed / runSpeed * .5f) * inputdir.magnitude;

        if (jumping || jumpStart)
        {
            animationSpeedPercent = ((jumping) ? 1f : animationSpeedPercent);
            animationSpeedPercent = ((jumpStart) ? 0.75f : animationSpeedPercent);
            speedSmoothTime = ((jumpStart || jumping) ? 0.07f : 0.001f);
        }
        if (transform.parent != null)
        {
            animationSpeedPercent = 0f;
            jumpcount = 0;
        }


        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

}
