using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float defDistanceRay = 50;
    [SerializeField] private LayerMask whatIsEnemy;

    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    bool laserActive = false;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        //RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right, defDistanceRay, whatIsEnemy);

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_lineRenderer.enabled = true;
            laserActive = true;

            StartCoroutine(LaserOff());
        }

        if (laserActive)
        {
            Draw2DRay(laserFirePoint.position, new Vector2(laserFirePoint.position.x * defDistanceRay, laserFirePoint.position.y));
        }
    }

    IEnumerator LaserOff()
    {
        yield return new WaitForSecondsRealtime(5f);
        m_lineRenderer.enabled = false;
        laserActive = false;
    }



    void Draw2DRay(Vector2 startPos, Vector2 endPos)   
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
