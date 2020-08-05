using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulancePath : MonoBehaviour
{
 
    public Transform pathHolder;
    public float speed=10f;
    public float waitTime=0f;
    public float turnSpeed=90f;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] waypoint= new Vector3[pathHolder.childCount];
        for (int index=0;index<waypoint.Length;index++){
            waypoint[index]=pathHolder.GetChild(index).position;
        }
        StartCoroutine(followPath(waypoint));   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator followPath(Vector3[] waypoint){
        transform.position=waypoint[0];
        int targetWayPointIndex=1;
        Vector3 targetWayPoint=waypoint[targetWayPointIndex];
        transform.LookAt(targetWayPoint);

        while (true){
            transform.position=Vector3.MoveTowards(transform.position,targetWayPoint,speed*Time.deltaTime);
            if (transform.position==targetWayPoint){
                targetWayPointIndex=(targetWayPointIndex+1)%waypoint.Length;
                targetWayPoint=waypoint[targetWayPointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWayPoint));

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

    IEnumerator TurnToFace(Vector3 lookTarget){
        Vector3 dirToLookTarget = (lookTarget-transform.position).normalized;
        float targetAngle= 90-Mathf.Atan2(dirToLookTarget.z,dirToLookTarget.x)*Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,targetAngle))>0.05f){
            float angle=Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetAngle,turnSpeed*Time.deltaTime);
            transform.eulerAngles=Vector3.up*angle;
            yield return null;
        }
    }

    
}
