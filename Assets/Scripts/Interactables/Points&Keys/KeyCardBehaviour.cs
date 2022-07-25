using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Core
{
    public class KeyCardBehaviour : MonoBehaviour
    {
        CanvasUI ui;

        private void Start()
        {
            ui = GameObject.FindObjectOfType<CanvasUI>();
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
