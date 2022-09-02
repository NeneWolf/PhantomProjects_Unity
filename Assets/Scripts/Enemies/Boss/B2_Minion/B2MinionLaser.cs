using PhantomProjects.Core;
using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2MinionLaser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    [SerializeField] Transform laserFirePoint;
    public LineRenderer line;

    [SerializeField] GameObject boss;
    [SerializeField] GameObject parent;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] LayerMask whatIsGround;

    [SerializeField] float speed;
    [SerializeField] float damage;

    [SerializeField] ParticleSystem particles;

    RaycastHit2D damageHit;
    RaycastHit2D groundHit;
    bool hitPlayer = false;
    bool hasRotated = false;

    Vector3 currentAngle;
    Vector3 originalAngle;
    Vector3 targetAngle = new Vector3(0f, 0f, 0f);

    DifficultyManager difficulty;

    private void Awake()
    {
        hasRotated = false;
        if (particles != null)
            particles = Instantiate(particles, this.transform);

        difficulty = GameObject.FindObjectOfType<DifficultyManager>().GetComponent<DifficultyManager>();
        damage *= difficulty.difficultyMultiplier;
    }

    void Update()
    {
        currentAngle = parent.transform.eulerAngles;
        RotateToOrigin();
        ShootLaser();

        if (hitPlayer)
        {
            damageHit.transform.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            hitPlayer = false;
        }
    }

    void ShootLaser()
    {
        damageHit = Physics2D.Raycast(laserFirePoint.position, transform.right, defDistanceRay, whatIsPlayer);
        groundHit = Physics2D.Raycast(laserFirePoint.position, transform.right, defDistanceRay, whatIsGround);

        Draw2DRay(laserFirePoint.position, groundHit.point);
        particles.transform.position = groundHit.point;

        if (damageHit)
        {
            hitPlayer = true;
        }
    }


    void RotateToOrigin()
    {
        var step = speed * Time.deltaTime;

        if (!hasRotated)
        {
            
            if (boss.GetComponent<Entity>().facingDirection == 1)
            {
                currentAngle = new Vector3(
                        Quaternion.identity.x,
                        Quaternion.identity.y,
                        Mathf.LerpAngle(currentAngle.z, targetAngle.z, step));
                parent.transform.eulerAngles = currentAngle;
            }
            else if (boss.GetComponent<Entity>().facingDirection == -1)
            {
                currentAngle = new Vector3(
                        Quaternion.identity.x,
                        Quaternion.identity.y - 180,
                        Mathf.LerpAngle(currentAngle.z, targetAngle.z, step));

                parent.transform.eulerAngles = currentAngle;
            }

            StartCoroutine(TurnOffLaser());
        }

    }

    IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(4f);
        hasRotated = true;
        this.gameObject.SetActive(false);
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    public void SetFire(bool fire)
    {
        hasRotated = fire;
    }
}
