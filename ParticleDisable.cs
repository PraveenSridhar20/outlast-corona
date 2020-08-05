using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDisable : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraTrans;
    ParticleSystem particleSystem;
    void Start()
    {
        particleSystem=GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(cameraTrans.position.x-transform.position.x)>60){
            particleSystem.enableEmission=false;
        }
        else
            particleSystem.enableEmission=true;
    }
}
