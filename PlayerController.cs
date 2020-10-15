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
    float jumpcount=0f;
    public float jumpHeight=3f;
    float velocityY;
    float turnSmoothVelocity;
    public float speedSmoothTime=0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    Animator animator;
    CharacterController characterController;
    bool jumping=false;
    bool jumpStart=false;
    float timeFromJumpStart=0f;
    public float controlTime=1.1f;
    float timeToFall=0f;
    float timeFromKnockBack=0f;
    public static bool onGround=false;
    public static int lives=3;
    Vector3 platformPosition;
    public Text stats;
    public static float pills=0f;
    public Vector3 prevTrans;
    public static bool disabled=false;
    public static float[] startPosX = { -135, 29, 187, 336, 438};
    public static float[] startPosY = { 8.587f, 16f, 8.587f,24f,12f};
    public static int startInd=0;
    public static int deaths = 0;
    
    void Start()
    {
        animator=GetComponent<Animator>();
        characterController=GetComponent<CharacterController>(); 
        CrowdHitBox.onPlayerCollisionCrowd+=knockBack;
        disabled=false;
        transform.position = new Vector3(startPosX[startInd], startPosY[startInd], transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        onGround=characterController.isGrounded;
        if (timeFromJumpStart>0.04f){
            jumpStart=false;
            timeFromJumpStart=0;
        }
        
        Vector2 input;

        if (timeToFall<controlTime){
            timeToFall+=Time.deltaTime;
            jumpcount=2;
            input=new Vector2(0,0);
        }
        else{
            input=new Vector2(Input.GetAxisRaw("Horizontal"),0);
        }

        Vector2 inputdir=input.normalized;
        if (Input.GetKeyDown(KeyCode.Space)&&jumpcount<2f){
            transform.parent=null;
            Jump();
            jumpStart=true;
            jumpcount++;
            jumping=true;               
        }

        if (jumpStart)
            timeFromJumpStart+=Time.deltaTime;
        
        if (inputdir!=Vector2.zero){
            float targetRotation=Mathf.Atan2(inputdir.x,inputdir.y)*Mathf.Rad2Deg;
            transform.eulerAngles=Vector3.up*Mathf.SmoothDampAngle(transform.eulerAngles.y,targetRotation,ref turnSmoothVelocity,turnSmoothTime);
        }

        bool walking=Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
        if (Victory.firstTime)
            walking=true;
        float targetSpeed=((walking)?walkSpeed:runSpeed)*inputdir.magnitude;

        currentSpeed=Mathf.SmoothDamp(currentSpeed,targetSpeed,ref speedSmoothVelocity,speedSmoothTime);
        
        velocityY+=Time.deltaTime*gravity;
        
        Vector3 velocity=transform.forward*currentSpeed+Vector3.up*velocityY;
        Vector3 vel=new Vector3(velocity.x,velocity.y,0f);
        if (timeFromKnockBack>0&&timeFromKnockBack<0.4f){
            timeFromKnockBack+=Time.deltaTime;
            int kb=-1;
            if (transform.rotation.eulerAngles.y>=250)
                kb=1;
            vel=new Vector3(10*kb,6,0);
        }
        else {
            timeFromKnockBack=0f;
        }
        if (disabled)
            vel=Vector3.zero;
        characterController.Move(vel*Time.deltaTime);
        currentSpeed=new Vector2(characterController.velocity.x,characterController.velocity.z).magnitude;
        if (transform.parent==null){
            if (Mathf.Abs(transform.position.z-7.38f)>=0.001){
                Vector3 vector3=new Vector3(transform.position.x,transform.position.y,7.38f);
                transform.position=vector3;                
            }
        }
        else {
            
            platformPosition=new Vector3(transform.parent.position.x,transform.parent.position.y+1.9f,transform.parent.position.z);
            transform.position=platformPosition;
           // prevTrans=transform.position;
        }
        

        if (!characterController.isGrounded){
            if (!jumping)
                jumpcount=1;
            jumping=true;       
        }

        if (characterController.isGrounded){
            velocityY=0;
            jumpcount=0f;
            jumping=false;
        }
        float animationSpeedPercent=((walking)?currentSpeed/walkSpeed*.25f:currentSpeed/runSpeed*.5f)*inputdir.magnitude;
        
        if (jumping||jumpStart){
            animationSpeedPercent=((jumping)?1f:animationSpeedPercent);
            animationSpeedPercent=((jumpStart)?0.75f:animationSpeedPercent);
            speedSmoothTime=((jumpStart||jumping)?0.07f:0.001f);
        }
        if (transform.parent!=null){
            animationSpeedPercent=0f;
            jumpcount=0;
        }
        if (disabled)
            animationSpeedPercent=0f;
        
        animator.SetFloat("speedPercent",animationSpeedPercent,speedSmoothTime,Time.deltaTime);
        string s="Lives x";
        s+=lives.ToString();
        s+="\nPills x";
        s+=pills.ToString();
        s+="\nMask ";
        if (MaskSpin.shieldEnabled){
            float f=(16f-Shield.currentTime);
            int x=(int)f;
            s+=x.ToString();

        }
        else
            s+="0";
        s+="s\n";
        s += "Deaths x";
        s += deaths.ToString();
        stats.text=s;
    }

    void Jump(){
        
            float jumpVelocity=Mathf.Sqrt(-2*gravity*jumpHeight);
            velocityY=jumpVelocity;
        
    }

    void knockBack(){
        timeFromKnockBack+=Time.deltaTime;   //you can set velocityY to some negative value here
                                            //this will make the player a smooth projectile
    }

    void OnDestroy(){
        CrowdHitBox.onPlayerCollisionCrowd-=knockBack;
        string s="Lives x";
        s+="0";
        s+="\nPills x";
        s+=pills.ToString();
        s+="\nMask ";
        if (MaskSpin.shieldEnabled)
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