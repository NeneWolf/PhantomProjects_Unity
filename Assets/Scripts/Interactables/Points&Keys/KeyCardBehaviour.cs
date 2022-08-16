using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;

namespace PhantomProjects.Core
{
    public class KeyCardBehaviour : MonoBehaviour
    {
        UIManager ui;
        [SerializeField] LayerMask whatIsPlayer;

        private void Awake()
        {
            ui = GameObject.FindObjectOfType<UIManager>();
        }

        private void Update()
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x);

            if (playerCollider && Input.GetKeyDown(KeyCode.F))
            {
                ui.KeyCardCollection();
                Destroy(this.gameObject);
            }
        }
    }
}
