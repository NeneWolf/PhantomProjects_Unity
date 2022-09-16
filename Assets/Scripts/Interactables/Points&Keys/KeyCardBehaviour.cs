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

        [SerializeField] bool SpecialKeyCard;
        [SerializeField] SpriteRenderer keyCardSprite;
        [SerializeField] GameObject Boss;

        private void Awake()
        {
            ui = GameObject.FindObjectOfType<UIManager>();

            if(SpecialKeyCard)
                keyCardSprite.enabled = false;
        }

        private void Update()
        {
            if (!SpecialKeyCard)
            {
                Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x, whatIsPlayer);

                if (playerCollider && Input.GetKeyDown(KeyCode.F))
                {
                    ui.KeyCardCollection();
                    Destroy(this.gameObject);
                }
            }
            else if (SpecialKeyCard)
            {
                if (Boss.activeInHierarchy == false)
                {
                    keyCardSprite.enabled = true;

                    Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x, whatIsPlayer);

                    if (playerCollider && Input.GetKeyDown(KeyCode.F))
                    {
                        ui.KeyCardCollection();
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
