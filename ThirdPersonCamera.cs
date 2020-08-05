using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Start is called before the first frame update
    float yaw;
    float pitch;
    public float sensitivity=10;
    public Transform target;
    public float distanceFromTarget;
    public Vector2 pitchMinMax=new Vector2(-40,85);
    public float rotationSmoothTime=0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        yaw+=Input.GetAxis("Mouse X")*sensitivity;
        pitch-=Input.GetAxis("Mouse Y")*sensitivity;
        pitch=Mathf.Clamp(pitch,pitchMinMax.x,pitchMinMax.y);
        currentRotation=Vector3.SmoothDamp(currentRotation,new Vector3(pitch,yaw),ref rotationSmoothVelocity,rotationSmoothTime);
        transform.eulerAngles=currentRotation;
        transform.position=target.position-transform.forward*distanceFromTarget;

    }
}
