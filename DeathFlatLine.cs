using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlatLine : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        CrowdHitBox.onPlayerCollisionCrowdDeath+=audioSource.Play;
        VirusPath.onPlayerCollision+=audioSource.Play;
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        CrowdHitBox.onPlayerCollisionCrowdDeath-=audioSource.Play;
        VirusPath.onPlayerCollision-=audioSource.Play;
    }
}
