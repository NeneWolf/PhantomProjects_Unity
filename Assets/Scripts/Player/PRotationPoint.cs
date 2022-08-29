using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRotationPoint : MonoBehaviour
{
    GameObject controls;
    int direction = 1;

    private void Awake()
    {
        controls = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if(direction != controls.GetComponent<PrototypeHero>().m_facingDirection)
        {
            direction = controls.GetComponent<PrototypeHero>().m_facingDirection;
            transform.Rotate(0f, 180f, 0f);
        }
        else if(direction == controls.GetComponent<PrototypeHero>().m_facingDirection && transform.rotation.y != 0f)
        {
            transform.Rotate(0f, 0f, 0f);
        }
            
    }
}
