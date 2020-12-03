using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivity=10f;
    public float speed = 3;
    public Vector2 pitchMinMax = new Vector2(-40, 75);
    public float rotationSmoothTime = 0.12f;
    public AudioSource audioSource;

    float yaw;
    float pitch;
    Transform cameraT;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    float verticalLookRotation;
    bool crouched=false;
    float ypos=1.717f;
    bool walking=false;
    bool disabled = false;
    bool contd=false;

    Vector3 inputMove;
    CharacterController characterController;
    void Start()
    {
        characterController=GetComponent<CharacterController>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        crouchInput();
        mouseInput();
        if (!disabled) {
            keyboardInput();
        }
        walkSound();
        
        
    }

    void crouchInput() {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (crouched)
            {
                crouched = false;
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if (!crouched && ypos > 1f)
            {
                crouched = true;
                transform.Translate(Vector3.up * Time.deltaTime * -speed);
            }
        }
        if (crouched)
            ypos = 1f;
        else
            ypos = 1.717f;

        if (!crouched && transform.position.y < 1.717f)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            ypos = 1.717f;
            disabled = true;
        }
        else if (crouched && transform.position.y > 1f)
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
            ypos = 1f;
            disabled = true;
        }

        if (!crouched && transform.position.y >= 1.717f) {
            disabled = false;
        }
        else if (crouched && transform.position.y <= 1f)
        {
            disabled = false;
        }
    }

    void mouseInput() {

        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw, 0), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;
    }

    void keyboardInput() {

        Vector3 vector3 = transform.forward * speed * (Input.GetAxisRaw("Vertical")) + transform.right * speed * (Input.GetAxisRaw("Horizontal"));
        vector3 = new Vector3(vector3.x, 0, vector3.z);
        if (vector3.magnitude > 0 && characterController.velocity.magnitude>0.1f)
        {
            walking = true;
        }
        else
            walking = false;
        characterController.Move(vector3 * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, ypos, transform.position.z);
    }

    void walkSound() {
        if (walking && !contd)
        {
            audioSource.Play();
            contd = true;
        }
        else if (walking && contd)
        {
            contd = true;
        }
        else if (!walking)
        {
            contd = false;
            audioSource.Stop();
        }
    }
}
