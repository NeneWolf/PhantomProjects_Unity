using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBehaviour : MonoBehaviour
{
    private AttackDetails attackDetails;

    //[SerializeField] GameObject boss;
    [SerializeField] private float defDistanceRay = 100;
    [SerializeField] private float damage = 5f;
    [SerializeField] private LayerMask whatIsPlayer;
    public LineRenderer line;

    Transform lazerFirePoint;
 
    GameObject target;

    private void Awake()
    {
        lazerFirePoint = GameObject.Find("Mini_Boss_2").transform.Find("Alive").Find("RangeAttackPosition").transform;
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        ShootLazer();
    }

    void ShootLazer()
    {
        RaycastHit2D damageHit = Physics2D.Raycast(lazerFirePoint.position, transform.right, defDistanceRay, whatIsPlayer);

        if (damageHit)
        {
            if(lazerFirePoint.position.x > damageHit.point.x)
                Draw2DRay(lazerFirePoint.position, new Vector2(damageHit.point.x * -defDistanceRay, damageHit.point.y));
            else
                Draw2DRay(lazerFirePoint.position, new Vector2(damageHit.point.x * defDistanceRay, damageHit.point.y));

            attackDetails.damageAmount = damage;
            StartCoroutine(TakeDamageAndDestroy());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    IEnumerator TakeDamageAndDestroy()
    {
        target.GetComponent<PlayerStats>().TakeDamage(attackDetails);
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
