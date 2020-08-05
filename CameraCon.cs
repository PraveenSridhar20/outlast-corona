using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    // Start is called before the first frame update
   
    public Transform playerTrans;
    float currentTime=0f;
    public float timeControl=1.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.Translate(Input.GetAxisRaw("Horizontal")*speed*Time.deltaTime,0,0);
        //currentTime+=Time.deltaTime;
        if (currentTime<timeControl){
            currentTime+=Time.deltaTime;
        }
        else{
            Vector3 v1=new Vector3(playerTrans.position.x+5,playerTrans.position.y+2f,0);
            transform.position=v1;
        }
    }
}
