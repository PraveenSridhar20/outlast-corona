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
    Transform cameraT;
    void Start()
    {
        animator=GetComponent<Animator>();
        characterController=GetComponent<CharacterController>(); 
        cameraT=Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
    
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
            input=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        }

        Vector2 inputdir=input.normalized;
        if (Input.GetKeyDown(KeyCode.Space)&&jumpcount<2f){
            Jump();
            jumpStart=true;
            jumpcount++;
            jumping=true;   
            transform.parent=null;

            
        }

        if (jumpStart)
            timeFromJumpStart+=Time.deltaTime;
        
        if (inputdir!=Vector2.zero){
            float targetRotation=Mathf.Atan2(inputdir.x,inputdir.y)*Mathf.Rad2Deg+cameraT.eulerAngles.y;
            transform.eulerAngles=Vector3.up*Mathf.SmoothDampAngle(transform.eulerAngles.y,targetRotation,ref turnSmoothVelocity,turnSmoothTime);
        }

        bool walking=Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
        float targetSpeed=((walking)?walkSpeed:runSpeed)*inputdir.magnitude;

        currentSpeed=Mathf.SmoothDamp(currentSpeed,targetSpeed,ref speedSmoothVelocity,speedSmoothTime);
        
        velocityY+=Time.deltaTime*gravity;
        
        Vector3 velocity=transform.forward*currentSpeed+Vector3.up*velocityY;
        Vector3 vel=new Vector3(velocity.x,velocity.y,velocity.z);
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
        //vel=new Vector3(0,vel.y,0);
        characterController.Move(vel*Time.deltaTime);
        currentSpeed=new Vector2(characterController.velocity.x,characterController.velocity.z).magnitude;
        transform.position=new Vector3(0,transform.position.y,0);

        if (characterController.isGrounded){
            velocityY=0;
            jumpcount=0f;
            jumping=false;
        }

        if (!characterController.isGrounded){
            if (!jumping)
                jumpcount=1;
            jumping=true;       
        }

        float animationSpeedPercent=((walking)?.25f:.5f)*inputdir.magnitude;
    
        if (jumping||jumpStart){
            animationSpeedPercent=((jumping)?1f:animationSpeedPercent);
            animationSpeedPercent=((jumpStart)?0.75f:animationSpeedPercent);
            speedSmoothTime=((jumpStart||jumping)?0.07f:0.001f);
        }
        
        animator.SetFloat("speedPercent",animationSpeedPercent,speedSmoothTime,Time.deltaTime);
      
    }

    void Jump(){
        
            float jumpVelocity=Mathf.Sqrt(-2*gravity*jumpHeight);
            velocityY=jumpVelocity;
        
    }

    void knockBack(){
        timeFromKnockBack+=Time.deltaTime;   
    
    }

    void OnDestroy(){
      
        
    }
}
