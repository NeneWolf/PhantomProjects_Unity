using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerPosition : MonoBehaviour
{
    GameObject target;

    bool hasMovePlayer = false;

    private void Update()
    {
        if (hasMovePlayer == false)
            StartCoroutine(waitBeforePutting());
    }

    IEnumerator waitBeforePutting()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        target = GameObject.FindGameObjectWithTag("Player").gameObject;
        target.transform.position = this.gameObject.transform.position;
        hasMovePlayer = true;
    }

}
