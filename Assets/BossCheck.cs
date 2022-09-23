using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject doubleJumpTrigger;
    bool hasTrigger = false;
    
    // Update is called once per frame
    void Update()
    {
        if(boss.gameObject.active == false && hasTrigger == false)
        {
            hasTrigger = true;
            platform.gameObject.SetActive(true);
            doubleJumpTrigger.gameObject.SetActive(true);
        }
    }
}
