using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerPosition : MonoBehaviour
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        target.transform.position = this.gameObject.transform.position;
    }

}
