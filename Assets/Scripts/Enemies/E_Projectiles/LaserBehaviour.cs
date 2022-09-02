using PhantomProjects.Core;
using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    Transform rayPosition;
    [SerializeField] Transform laserSpawnPoint;
    [SerializeField] private float defDistanceRay = 25f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] LineRenderer line;

    GameObject target;

    DifficultyManager difficulty;

    private void Awake()
    {
        transform.position = GameObject.Find("/Mini_Boss_ME16/Alive/RangeAttackPosition").transform.position;
        rayPosition = GameObject.Find("/Mini_Boss_ME16/Alive/PlayerCheck").transform;
        target = GameObject.FindGameObjectWithTag("Player");
        difficulty = GameObject.FindObjectOfType<DifficultyManager>().GetComponent<DifficultyManager>();
        damage *= difficulty.difficultyMultiplier;

        ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D damageHit = Physics2D.Raycast(rayPosition.position, transform.right, defDistanceRay, whatIsPlayer);

        if (damageHit)
        {
            if (transform.position.x > damageHit.point.x)
                Draw2DRay(transform.position, target.transform.position);
            else
                Draw2DRay(transform.position, target.transform.position);

            StartCoroutine(TakeDamageAndDestroy());
        }
        else
        {
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    IEnumerator TakeDamageAndDestroy()
    {
        target.GetComponent<PlayerStats>().TakeDamage(damage);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
