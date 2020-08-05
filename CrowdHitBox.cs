using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrowdHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particles;
    public static event System.Action onPlayerCollisionCrowd;
    public static event System.Action onPlayerCollisionCrowdDeath;
    public Transform playerTrans;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Mathf.Abs(playerTrans.position.y-transform.position.y));
        if(Mathf.Abs(playerTrans.position.x-transform.position.x)<1.5f&&!(Blinking.blink)&&(Mathf.Abs(playerTrans.position.y-transform.position.y)<1f)&&!(MaskSpin.shieldEnabled)&&(PlayerController.lives>0)){
            PlayerController.lives--;
            print(PlayerController.lives);
            if (onPlayerCollisionCrowd!=null)
                onPlayerCollisionCrowd();
        }
        else if (Mathf.Abs(playerTrans.position.x-transform.position.x)<1.5f&&!(Blinking.blink)&&(Mathf.Abs(playerTrans.position.y-transform.position.y)<1f)&&!(MaskSpin.shieldEnabled)&&(PlayerController.lives<=0)){
            particles.enableEmission=true;
            particles.Play();
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Destroy(GameObject.FindGameObjectWithTag("ME"));
            PlayerController.lives=3;
            if (onPlayerCollisionCrowdDeath!=null)
                onPlayerCollisionCrowdDeath();
            
        }
    }
    void OnTriggerEnter(Collider triggerCollider){
        //print(triggerCollider.gameObject.name);
        
        if ((triggerCollider.tag=="ME"||triggerCollider.tag=="Player")&&(PlayerController.lives>0)){
            
            //particles.enableEmission=true;
            //Destroy(GameObject.FindGameObjectWithTag("Player"));
            //Destroy(GameObject.FindGameObjectWithTag("ME"));   
            print ("Collided at:"+(playerTrans.position.y-transform.position.y));
            PlayerController.lives--;
            print(PlayerController.lives);
            if (onPlayerCollisionCrowd!=null)
                onPlayerCollisionCrowd();
        }
        else if ((triggerCollider.tag=="ME"||triggerCollider.tag=="Player")&&(PlayerController.lives<=0)){

            particles.enableEmission=true;
            particles.Play();
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Destroy(GameObject.FindGameObjectWithTag("ME"));
            PlayerController.lives=3;
            if (onPlayerCollisionCrowdDeath!=null)
                onPlayerCollisionCrowdDeath();
        }
    }
}   
