﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float angularvel=10f;
    public ParticleSystem coinColl;
    public AudioSource audioSource;
    public AudioClip audioClip;
    bool collected=false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        coinColl.enableEmission=false;
        audioSource.clip=audioClip;
        audioSource.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        float angulardisp=angularvel*Time.deltaTime;
        transform.RotateAround(Vector3.up,angulardisp);
        
    }

     void OnTriggerEnter(Collider triggerCollider){
        //print(triggerCollider.gameObject.name);
       
        if (triggerCollider.tag=="ME"||triggerCollider.tag=="Player"||triggerCollider.tag=="PlayerIn"||triggerCollider.tag=="MEIn"||triggerCollider.tag=="PlayerMask"||triggerCollider.tag=="MEMask"){
            audioSource.Play();
            collected=true;
            Destroy(this.gameObject);
            coinColl.enableEmission=true;
            coinColl.Play();
        }
    }

    void OnDestroy(){
        if (collected){
            PlayerController.pills++;
        }
    }
   
}
