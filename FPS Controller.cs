using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivity=10f;
    float yaw;
    float pitch;
    Transform cameraT;
    public Vector2 pitchMinMax=new Vector2(-40,75);
    public float rotationSmoothTime=0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public float speed=3;
    float verticalLookRotation;
    bool crouched=false;
    float ypos=1.717f;
    bool walking=false;
    public AudioSource audioSource;
    bool contd=false;
    //Rigidbody rigidbody;

    Vector3 inputMove;
    CharacterController characterController;
    void Start()
    {
        //cameraT=Camera.main.transform;
        //rigidbody=GetComponent<Rigidbody>();
        characterController=GetComponent<CharacterController>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //inputMove=new Vector3(0,0,Input.GetAxisRaw("Vertical"));
        
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            if (crouched){
                crouched=false;
                transform.Translate(Vector3.up*speed*Time.deltaTime);
            }
            else if (!crouched&&ypos>1f){
                crouched=true;
               transform.Translate(Vector3.up*Time.deltaTime*-speed);
            }
        }
        if (crouched)
            ypos=1f;
        else
            ypos=1.717f;

        
        yaw+=Input.GetAxis("Mouse X")*sensitivity;
        pitch-=Input.GetAxis("Mouse Y")*sensitivity;
        pitch=Mathf.Clamp(pitch,pitchMinMax.x,pitchMinMax.y);
        currentRotation=Vector3.SmoothDamp(currentRotation,new Vector3(pitch,yaw,0),ref rotationSmoothVelocity,rotationSmoothTime);
        transform.eulerAngles=currentRotation;

    
        if (!crouched&&transform.position.y<1.717f){
            transform.Translate(Vector3.up*speed*Time.deltaTime);
            ypos=1.717f;
        }
        else if (crouched &&transform.position.y>1f){
            transform.Translate(-Vector3.up*speed*Time.deltaTime);
            ypos=1f;
        }
        else{
            Vector3 vector3=transform.forward*speed*(Input.GetAxisRaw("Vertical"));
            Vector3 vector31=transform.right*speed*(Input.GetAxisRaw("Horizontal"));
            vector3=vector3+vector31;
            if (vector3.magnitude>0){
                walking=true;
            }
            else    
                walking=false;
            characterController.Move(vector3*Time.deltaTime);
            transform.position=new Vector3(transform.position.x,ypos,transform.position.z);
        }
        //transform.Translate(inputMove*speed*Time.deltaTime);
        if (walking&&!contd){
            audioSource.Play();
            contd=true;
        }
        else if (walking&&contd){
            contd=true;
        }
        else if (!walking){
            contd=false;
            audioSource.Stop();
        }

        
    }
}
