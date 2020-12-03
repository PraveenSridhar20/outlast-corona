using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer meshRenderer;
    public static float currentTime=0f;
    public float shieldDuration=10f;
    GameObject gameObject1,gameObject2;
    public static bool shieldEnabled;
    void Start()
    {
        meshRenderer=GetComponent<MeshRenderer>();
        meshRenderer.enabled=false;
        shieldEnabled = false;
        gameObject1=GameObject.FindGameObjectWithTag("Player");
        gameObject2=GameObject.FindGameObjectWithTag("ME");
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldEnabled&&currentTime<=shieldDuration){
            meshRenderer.enabled=true;
            currentTime+=Time.deltaTime;
        }
        else if (currentTime>shieldDuration){
            currentTime=0;
            meshRenderer.enabled=false;
            shieldEnabled=false;
            gameObject1.tag="Player";
            gameObject2.tag="ME";
            Blinking.startBlink();
        }
    }
}
