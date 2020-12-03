using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed=2;
    public float runSpeed=6;
    public float turnSmoothTime=0.001f;
    public float gravity=-2f;
    public float jumpHeight = 3f;
    public float speedSmoothTime = 0.1f;
    public float controlTime = 1.1f;

    public Text stats;
    public static float pills = 0f;
    public Vector3 prevTrans;
    public static bool disabled = false;
    public static float startPosX = -135;
    public static float startPosY = 8.587f;
    public static int deaths = 0;
    public static int lives = 3;

    float jumpcount=0f;
    float velocityY;
    float currentSpeed;
    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float timeFromJumpStart = 0f;
    float timeToFall = 0f;
    float timeFromKnockBack = 0f;
    bool walking;
    bool jumping = false;
    bool jumpStart = false;
    Animator animator;
    CharacterController characterController;
    Vector3 platformPosition;
    Vector2 inputdir;
    Vector3 vel;


    void Start()
    {
        animator=GetComponent<Animator>();
        characterController=GetComponent<CharacterController>(); 
        CrowdHitBox.onPlayerCollisionCrowd+=knockBack;
        disabled=true;
        transform.position = new Vector3(startPosX, startPosY, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {

        initialFall();
        horizontalInput();
        verticalInput();
        calculateVelocity();
        characterControllerAssignment();
        animate();
        setHUD();
    }

    void initialFall() {
        if (timeToFall < controlTime && timeToFall >= 0)
        {
            timeToFall += Time.deltaTime;
            disabled = true;
        }
        else if (timeToFall >= controlTime)
        {
            timeToFall = -1;
            disabled = false;
        }
    }


    void horizontalInput() {
        inputdir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        if (disabled)
            inputdir = new Vector2(0, 0);
        inputdir = inputdir.normalized;
        if (inputdir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputdir.x, inputdir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        walking = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (Victory.firstTime)
            walking = true;
        float targetSpeed = ((walking) ? walkSpeed : runSpeed) * inputdir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
    }

    void verticalInput() {

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

    void Jump(){
        if (!disabled)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            jumpStart = true;
            jumpcount++;
            jumping = true;
        }
    }

    void knockBack(){
        timeFromKnockBack+=Time.deltaTime;   //you can set velocityY to some negative value here
                                            //this will make the player a smooth projectile
    }

    void calculateVelocity() {
        velocityY += Time.deltaTime * gravity;
        vel = transform.forward * currentSpeed + Vector3.up * velocityY;
        if (timeFromKnockBack > 0 && timeFromKnockBack < 0.4f)//if knockedback
        {
            timeFromKnockBack += Time.deltaTime;
            int kb = -1;
            if (transform.rotation.eulerAngles.y >= 250)
                kb = 1;
            vel = new Vector3(10 * kb, 6, 0);
        }
        else
        {
            timeFromKnockBack = 0f;
        }
        
    }

    void characterControllerAssignment() {
        characterController.Move(vel * Time.deltaTime);
        currentSpeed = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;
        if (transform.parent == null)
        {
            if (Mathf.Abs(transform.position.z - 7.38f) >= 0.001)
            {
                Vector3 vector3 = new Vector3(transform.position.x, transform.position.y, 7.38f);//adjusting z to make sure player does not stray away 
                transform.position = vector3;
            }
        }
        else
        {
            //if on platform
            platformPosition = new Vector3(transform.parent.position.x, transform.parent.position.y + 1.9f, transform.parent.position.z);
            transform.position = platformPosition;
        }

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

    void animate() {

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

    void setHUD()
    {
        string s = "Lives x";
        s += lives.ToString();
        s += "\nPills x";
        s += pills.ToString();
        s += "\nMask ";
        if (Shield.shieldEnabled)
        {
            float f = (16f - Shield.currentTime);
            int x = (int)f;
            s += x.ToString();

        }
        else
            s += "0";
        s += "s\n";
        s += "Deaths x";
        s += deaths.ToString();
        stats.text = s;
    }

    void OnDestroy(){
        CrowdHitBox.onPlayerCollisionCrowd-=knockBack;
        string s="Lives x";
        s+="0";
        s+="\nPills x";
        s+=pills.ToString();
        s+="\nMask ";
        if (Shield.shieldEnabled)
            s+=(15f-Shield.currentTime).ToString();
        else
            s+="0";
        s+="s\n";
        s += "Deaths x";
        deaths++;
        s += deaths.ToString();
        stats.text=s;
    }

    
}