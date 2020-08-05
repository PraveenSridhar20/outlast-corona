using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        audioSource.Play();
        //VirusPath.onPlayerCollision+=audioSource.Stop;
       // CrowdHitBox.onPlayerCollisionCrowdDeath+=audioSource.Stop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnDestroy(){
        //VirusPath.onPlayerCollision-=audioSource.Stop;
       // CrowdHitBox.onPlayerCollisionCrowdDeath-=audioSource.Stop;
    }
}
