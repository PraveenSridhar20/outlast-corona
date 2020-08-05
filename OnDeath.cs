using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem particleSystem;
    public Transform playerTrans;

    void Start()
    {
        particleSystem=GetComponent<ParticleSystem>();
        particleSystem.enableEmission=false;
    }

    // Update is called once per frame
    void Update()
    {   
        
        Vector3 v1=new Vector3(playerTrans.position.x-5,playerTrans.position.y-2.5f,7.83f);
        transform.position=v1;
    }
}
