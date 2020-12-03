using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "ME" || triggerCollider.tag == "Player" || triggerCollider.tag == "PlayerIn" || triggerCollider.tag == "MEIn" || triggerCollider.tag == "PlayerMask" || triggerCollider.tag == "MEMask")
        {
            PlayerController.startPosX = transform.position.x;
            PlayerController.startPosY = transform.position.y;
            print("Collided here");
        }
    }
}
