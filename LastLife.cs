using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLife : MonoBehaviour
{
    AudioSource audioSource;
    bool flag=false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.lives==0&&!flag){
            audioSource.Play();
            flag=true;
        }
        else if (PlayerController.lives<0){
            audioSource.Stop();
            flag=false;
        }
        else if (PlayerController.lives>0){
            flag=false;
            audioSource.Stop();
        }

    }
}
