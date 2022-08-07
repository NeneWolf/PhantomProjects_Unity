using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField] List<Transform> points;
    [SerializeField] float moveSpeed = 2f;

    [Header("Check Player Under or Next to")]
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] Transform playerCheck;
    [SerializeField] float extraX = 0.1f;
    [SerializeField] float extraY = 0.3f;

    BoxCollider2D coll;

    int goalPoint = 0;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        MoveToNextPoint();

        CheckCollisions();
    }

    void CheckCollisions()
    {
        Collider2D playerUnderHit = Physics2D.OverlapBox(playerCheck.position, new Vector2(transform.lossyScale.x + extraX, transform.lossyScale.y + extraY), 0f, whatIsPlayer);

        if (playerUnderHit)
        {
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
    
    void MoveToNextPoint()
    {
        //Change position of the platform
        transform.position = Vector2.MoveTowards(transform.position, points[goalPoint].position, Time.deltaTime * moveSpeed);
        if (Vector2.Distance(transform.position, points[goalPoint].position) < 0.1f)
        {
            if (goalPoint == points.Count - 1)
            {
                goalPoint = 0;
            }
            else
            {
                goalPoint++;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(playerCheck.position, new Vector2(transform.lossyScale.x + extraX, transform.lossyScale.y + extraY));
    }
}
