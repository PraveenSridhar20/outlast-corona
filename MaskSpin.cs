using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSpin : MonoBehaviour
{
    // Start is called before the first frame update
     public float angularvel=10f;
    public ParticleSystem coinColl;
    public AudioSource audioSource;
    public AudioClip audioClip;

    GameObject gameObject1,gameObject2;
    public static bool shieldEnabled=false;
    
    // Start is called before the first frame update
    void Start()
    {
        coinColl.enableEmission=false;
        audioSource.clip=audioClip;
        audioSource.Stop();
        gameObject1=GameObject.FindGameObjectWithTag("Player");
        gameObject2=GameObject.FindGameObjectWithTag("ME");
    }

    // Update is called once per frame
    void Update()
    {
        float angulardisp=angularvel*Time.deltaTime;
        transform.RotateAround(Vector3.up,angulardisp);
        
    }

     void OnTriggerEnter(Collider triggerCollider){
       //    print(triggerCollider.gameObject.name);
       
        if (triggerCollider.tag=="ME"||triggerCollider.tag=="Player"||triggerCollider.tag=="MEIn"||triggerCollider.tag=="PlayerIn"){
            audioSource.Play();
            Destroy(this.gameObject);
            coinColl.enableEmission=true;
            coinColl.Play();
            gameObject1.tag="PlayerMask";
            gameObject2.tag="MEMask";
            shieldEnabled=true;            
        }
    }
}
