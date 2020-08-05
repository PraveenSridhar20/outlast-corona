using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    bool playing=true;
    public static bool firstTime=false;
    BoxCollider boxCollider;
    float timeFromCollision=0f;
    public float fadeSpeed=1f;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        firstTime=false;
        timeFromCollision=0f;
        audioSource.Stop();
        boxCollider=GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!firstTime&&!playing){
            audioSource.Play();
            firstTime=true;
            playing=true;
        }
        if (timeFromCollision>0f&&timeFromCollision<=1.5f){
            timeFromCollision+=Time.deltaTime;
        }
        else if (timeFromCollision>1.5f){
            boxCollider.isTrigger=false;
        }
    }
    void OnTriggerEnter(Collider triggerCollider){
        if (triggerCollider.tag=="ME"||triggerCollider.tag=="Player"||triggerCollider.tag=="PlayerIn"||triggerCollider.tag=="MEIn"||triggerCollider.tag=="PlayerMask"||triggerCollider.tag=="MEMask"){
            playing=false;
        }
    }
    void OnTriggerExit(Collider triggerCollider){
        if ((triggerCollider.tag=="ME"||triggerCollider.tag=="Player"||triggerCollider.tag=="PlayerIn"||triggerCollider.tag=="MEIn"||triggerCollider.tag=="PlayerMask"||triggerCollider.tag=="MEMask")&&firstTime){
            timeFromCollision+=Time.deltaTime;
        }
    }
    
}
