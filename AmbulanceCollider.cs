using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerTrans;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider triggerCollider){
        if (triggerCollider.tag=="ME"||triggerCollider.tag=="Player"||triggerCollider.tag=="PlayerIn"||triggerCollider.tag=="MEIn"||triggerCollider.tag=="PlayerMask"||triggerCollider.tag=="MEMask"){
            playerTrans.transform.parent=transform;
          //  Vector3 vector3=new Vector3(0,0,0);
          //  playerTrans.transform.position=vector3;
        }
    }

    void OnTriggerStay(Collider triggerCollider){
        if (triggerCollider.tag=="ME"||triggerCollider.tag=="Player"||triggerCollider.tag=="PlayerIn"||triggerCollider.tag=="MEIn"||triggerCollider.tag=="PlayerMask"||triggerCollider.tag=="MEMask"){
            playerTrans.transform.parent=transform;
           // Vector3 vector3=new Vector3(0,0,0);
           // playerTrans.transform.position=vector3;
        }
    }

    /*void OnTriggerExit(Collider triggerCollider){
        if (triggerCollider.tag=="ME"||triggerCollider.tag=="Player"||triggerCollider.tag=="PlayerIn"||triggerCollider.tag=="MEIn"||triggerCollider.tag=="PlayerMask"||triggerCollider.tag=="MEMask"){
            playerTrans.transform.parent=null;
        }
    }*/
}
