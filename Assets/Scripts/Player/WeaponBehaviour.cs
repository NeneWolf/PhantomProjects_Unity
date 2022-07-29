using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rigidbody2;

    [SerializeField] private LayerMask whatIsBoom;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        rigidbody2.gravityScale = 0.0f;
        rigidbody2.velocity = transform.right * speed;
    }

}
