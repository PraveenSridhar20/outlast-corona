using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchHandle : MonoBehaviour
{
    // Start is called before the first frame update
    bool lightOn=true;
    Light light;
    AudioSource audioSource;
    void Start()
    {
        light=GetComponent<Light>();
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if (lightOn){
                light.intensity=0f;
                lightOn=false;
                audioSource.Play();
            }
            else {
                light.intensity=2.1f;
                lightOn=true;
                audioSource.Play();
            }
        }        
    }
}
