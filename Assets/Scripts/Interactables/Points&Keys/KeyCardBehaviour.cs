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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F))
            {
                ui.KeyCardCollection();
                Destroy(this.gameObject);
            }
        }
    }
}
