using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryFireWorks : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem particleSystem;
    void Start()
    {
        particleSystem=GetComponent<ParticleSystem>();
        particleSystem.enableEmission=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Victory.firstTime){
            particleSystem.enableEmission=true;
        }
    }
}
