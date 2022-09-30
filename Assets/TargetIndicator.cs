using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    GameObject target;
    Animator arrowAnim;

    [SerializeField] float arrowDuration = 2f;
    [SerializeField] float arrowCooldown = 2f;
    private float arrowDurationCounter;
    private float arrowCooldownCounter;
    bool arrowActive = false;
    bool arrowReady = true;
    bool arrowOnCooldown = false;

    private void Start()
    {
        arrowAnim = arrow.GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            var dir = target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (target == null)
        {
            target = FindClosest();
        }

        if (arrowReady && Input.GetKeyDown(KeyCode.Q))
        {
            arrow.SetActive(true);
            arrowReady = false;
            arrowActive = true;
            StartCoroutine(ArrowDuration());
        }
        else if (!arrowReady && !arrowActive)
        {
            ArrowCooldownTimer();
        }

        if (arrowActive)
        {
            ArrowDurationTimer();
        }

        if (target.gameObject.tag == "KeyCard")
        {
            arrowAnim.SetBool("Key", true);
            arrowAnim.SetBool("Door", false);
        }
        else
        {
            arrowAnim.SetBool("Key", false);
            arrowAnim.SetBool("Door", true);
        }
    }

    //Find the closes key
    GameObject FindClosest()
    {
        GameObject[] keys;
        keys = GameObject.FindGameObjectsWithTag("KeyCard");

        GameObject door;
        door = GameObject.FindGameObjectWithTag("ExitDoor");
        
        GameObject closest = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        if (keys.Length > 0)
        {
            foreach (GameObject key in keys)
            {

                Vector3 diff = key.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = key;
                    distance = curDistance;
                }
            }
        }
        else
        {
            Vector3 diff = door.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            
            if (curDistance < distance)
            {
                closest = door;
                distance = curDistance;
            }
        }

        return closest;
    }
    
    IEnumerator ArrowDuration()
    {
        yield return new WaitForSeconds(arrowDuration);
        arrow.SetActive(false);
        arrowReady = false;
        arrowCooldownCounter = arrowCooldown;
        StartCoroutine(ArrowCooldown());
    }
    
    IEnumerator ArrowCooldown()
    {
        arrowOnCooldown = true;
        yield return new WaitForSeconds(arrowCooldown);
        arrowReady = true;
        arrowOnCooldown = false;
        arrowDurationCounter = arrowDuration;
    }

    private void ArrowDurationTimer()                                      // Method for shield duration timer
    {
        arrowDurationCounter -= Time.deltaTime;
    }

    private void ArrowCooldownTimer()                                      // Method for shield duration timer
    {
        arrowCooldownCounter -= Time.deltaTime;
    }
}
