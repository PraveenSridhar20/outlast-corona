using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFX : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem particleSystem;
    bool partEnabled;
    float currentTime=0f;
    void Start()
    {
        partEnabled=false;     
        particleSystem=GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particleSystem.isEmitting)
            partEnabled=true;
        if (partEnabled){
            currentTime+=Time.deltaTime;
        }
        if (currentTime>=2f){
            particleSystem.enableEmission=false;
            currentTime=0f;
        }
    }
}
