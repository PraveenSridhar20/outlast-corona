using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//remember to add in moving platforms. Use the same script minus the collision. Use cranes and crates
public class VirusPath : MonoBehaviour
{
    public Transform pathHolder;
    public float speed=10f;
    public float waitTime=0f;
    public ParticleSystem particles;
    public static event System.Action onPlayerCollision;
    bool shieldOff=false;
    float currentTime=0f;
    GameObject gameObject1,gameObject2;
    public MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject1=GameObject.FindGameObjectWithTag("Player");
        gameObject2=GameObject.FindGameObjectWithTag("ME");
        Vector3[] waypoint= new Vector3[pathHolder.childCount];
        for (int index=0;index<waypoint.Length;index++){
            waypoint[index]=pathHolder.GetChild(index).position;
        }
        StartCoroutine(followPath(waypoint));
    }

    // Update is called once per frame
    void Update()
    {   
      if (shieldOff&&currentTime>=0&&currentTime<1f){
          currentTime+=Time.deltaTime;
      }  
      else if (currentTime>=1f){
          currentTime=0;
          shieldOff=false;
          meshRenderer.enabled=false;
          Blinking.startBlink();
          gameObject1.tag="PlayerIn";
          gameObject2.tag="MEIn";
      }
       
    }

    IEnumerator followPath(Vector3[] waypoint){
        transform.position=waypoint[0];
        int targetWayPointIndex=1;
        Vector3 targetWayPoint=waypoint[targetWayPointIndex];

        while (true){
            transform.position=Vector3.MoveTowards(transform.position,targetWayPoint,speed*Time.deltaTime);
            if (transform.position==targetWayPoint){
                targetWayPointIndex=(targetWayPointIndex+1)%waypoint.Length;
                targetWayPoint=waypoint[targetWayPointIndex];
                yield return new WaitForSeconds(waitTime);

            }
            yield return null;
        }
    }

    void OnDrawGizmos(){
        Vector3 startPosition=pathHolder.GetChild(0).position;
        Vector3 previousPosition=startPosition;
        foreach(Transform waypoint in pathHolder){
            Gizmos.DrawSphere(waypoint.position,0.3f);
            Gizmos.DrawLine(previousPosition,waypoint.position);
            previousPosition=waypoint.position;
        }
        Gizmos.DrawLine(previousPosition,startPosition);
    }

    void OnTriggerEnter(Collider triggerCollider){
       // print(triggerCollider.gameObject.name);
        
        if (triggerCollider.tag=="ME"||triggerCollider.tag=="Player"){
            
            particles.enableEmission=true;
            particles.Play();
            PlayerController.lives=-1;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Destroy(GameObject.FindGameObjectWithTag("ME"));   
            if (onPlayerCollision!=null)
                onPlayerCollision();        
        }
        else if (triggerCollider.tag=="PlayerIn"||triggerCollider.tag=="MEIn"){
            
            particles.enableEmission=true;
            particles.Play();
            PlayerController.lives=-1;
            Destroy(GameObject.FindGameObjectWithTag("PlayerIn"));
            Destroy(GameObject.FindGameObjectWithTag("MEIn"));   
            if (onPlayerCollision!=null)
                onPlayerCollision();        
        }
        else if (triggerCollider.tag=="PlayerMask"||triggerCollider.tag=="MEMask"){
            Shield.shieldEnabled=false;
            shieldOff=true;
            Shield.currentTime=0f;
            
        }
        
    }
}
