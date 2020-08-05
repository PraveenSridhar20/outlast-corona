using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 pos=new Vector3(playerTransform.transform.position.x,playerTransform.position.y+0.928f,playerTransform.position.z);
        transform.position=pos;
        //print(gameObject.tag);
    }
     
}
