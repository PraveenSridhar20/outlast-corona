using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdCrashCough : MonoBehaviour
{   
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        CrowdHitBox.onPlayerCollisionCrowd+=audioSource.Play;     
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy (){
        CrowdHitBox.onPlayerCollisionCrowd-=audioSource.Play;
    }
}
