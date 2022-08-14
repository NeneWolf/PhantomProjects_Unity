using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;

namespace PhantomProjects.Core
{
    public class KeyCardBehaviour : MonoBehaviour
    {
        UIManager ui;

        private void Start()
        {
            ui = GameObject.FindObjectOfType<UIManager>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                ui.KeyCardCollection();
                Destroy(this.gameObject);
            }
        }
    }
}
