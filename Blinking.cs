using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    // Start is called before the first frame update
    public SkinnedMeshRenderer meshRenderer1,meshRenderer2,meshRenderer3,meshRenderer4,meshRenderer5;
    static bool visible=true;
    static float currentTime=0f;
    public static int blinkFreq=0;
    public static bool blink=false;
    static GameObject gameObject1,gameObject2;
    void Start()
    {
        currentTime = 0f;
        blink = false;
        CrowdHitBox.onPlayerCollisionCrowd+=startBlink;
        gameObject1=GameObject.FindGameObjectWithTag("Player");
        gameObject2=GameObject.FindGameObjectWithTag("ME");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0 && currentTime < 3)
        {
            currentTime += Time.deltaTime;
            if (blinkFreq < 10)
            {
                blinkFreq++;
                meshRenderer1.enabled = visible;
                meshRenderer2.enabled = visible;
                meshRenderer3.enabled = visible;
                meshRenderer4.enabled = visible;
                meshRenderer5.enabled = visible;

            }
            else
            {
                blinkFreq = 0;
                if (visible)
                    visible = false;
                else
                    visible = true;
                meshRenderer1.enabled = visible;
                meshRenderer2.enabled = visible;
                meshRenderer3.enabled = visible;
                meshRenderer4.enabled = visible;
                meshRenderer5.enabled = visible;
            }
        }
        else if (currentTime >= 3f)
        {
            currentTime = 0;
            meshRenderer1.enabled = true;
            meshRenderer2.enabled = true;
            meshRenderer3.enabled = true;
            meshRenderer4.enabled = true;
            meshRenderer5.enabled = true;
            gameObject1.tag = "Player";
            gameObject2.tag = "ME";
            blink = false;
        }
        else if (Shield.shieldEnabled) {
            currentTime = 0;
            meshRenderer1.enabled = true;
            meshRenderer2.enabled = true;
            meshRenderer3.enabled = true;
            meshRenderer4.enabled = true;
            meshRenderer5.enabled = true;
            gameObject1.tag = "PlayerMask";
            gameObject2.tag = "MEMask";
            blink = false;
        }
    }
    public static void startBlink(){
        visible=false;
        gameObject1.tag="PlayerIn";
        gameObject2.tag="MEIn";
        currentTime+=Time.deltaTime;
        blink=true;
    }
    
    void OnDestroy(){
        CrowdHitBox.onPlayerCollisionCrowd-=startBlink;
     
    }
}
