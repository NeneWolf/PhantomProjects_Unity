using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsControls : MonoBehaviour
{
    
    [SerializeField] GameObject[] differentMinions;
    [SerializeField] GameObject laser;

    [Header("Minion 1 ")]
    private float nextShot = 0.15f;
    [SerializeField] float fireDelay = 18f;

    [Header("Minion 2 ")]
    private float nextHeal = 0.15f;
    [SerializeField] float healDelay = 25f;

    [Header("Minion 3 ")]
    private float nextSpit = 0.15f;
    [SerializeField] float spitDelay = 10f;

    // Update is called once per frame
    void Update()
    {
        UpdateMinion1();
        UpdateMinion2();
        UpdateMinion3();

        //Disable the main minion control if all are killed
        if(differentMinions[0].GetComponent<B2Minion1Behaviour>().isDead && differentMinions[1].GetComponent<B2Minion1Behaviour>().isDead && differentMinions[2].GetComponent<B2Minion1Behaviour>().isDead)
        {
            this.gameObject.SetActive(false);
        }
    }

    void UpdateMinion1()
    {
        if (!differentMinions[0].GetComponent<B2Minion1Behaviour>().isDead && Time.time > nextShot)
        {
            differentMinions[0].GetComponent<B2Minion1Behaviour>().SetFire(false);
            laser.GetComponentInChildren<B2MinionLaser>().SetFire(false);
            nextShot = Time.time + fireDelay;
        }
        else if (differentMinions[0].GetComponent<B2Minion1Behaviour>().isDead)
        {
            differentMinions[0].SetActive(false);
        }
    }

    void UpdateMinion2()
    {
        if (!differentMinions[1].GetComponent<B2Minion2Behaviour>().isDead && Time.time > nextHeal)
        {
            differentMinions[1].GetComponent<B2Minion2Behaviour>().healBoss();
            nextHeal = Time.time + healDelay;
        }
    }

    void UpdateMinion3()
    {
        if (!differentMinions[2].GetComponent<B2Minion3Behaviour>().isDead && Time.time > nextSpit)
        {
            differentMinions[2].GetComponent<B2Minion3Behaviour>().FireToxicSpit();
            nextSpit = Time.time + spitDelay;
        }
    }
}
