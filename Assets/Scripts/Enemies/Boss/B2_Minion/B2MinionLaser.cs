using PhantomProjects.Core;
using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2MinionLaser : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] LayerMask whatIsPlayer;
    
    DifficultyManager difficulty;

    private void Awake()
    {
        difficulty = GameObject.FindObjectOfType<DifficultyManager>().GetComponent<DifficultyManager>();
        damage *= difficulty.difficultyMultiplier;
    }


    void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, new Vector2(23f, 0.5f), transform.rotation.x, whatIsPlayer);

        if (playerCollider)
        {
            playerCollider.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(23f, 0.5f));
    }
}
