using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject doubleJumpTrigger;
    
    // Update is called once per frame
    void Update()
    {
        if(boss.gameObject.active == false)
        {
            platform.gameObject.SetActive(true);
            doubleJumpTrigger.gameObject.SetActive(true);
        }
    }
}
