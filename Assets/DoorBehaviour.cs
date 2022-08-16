using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    [SerializeField] int requiredNumberOfKeys;
    [SerializeField] LayerMask whatIsPlayer;

    GameObject sceneManager;
    GameManager gameManager;
    UIManager uiManager;

    [SerializeField] bool nextScene = false;
    [SerializeField] bool previousScene = false;

    private void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
        //gameManager = GetComponent<GameManager>();
        //uiManager = GetComponent<UIManager>();
    }

    void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.x ,whatIsPlayer);

        if (playerCollider && Input.GetKeyDown(KeyCode.F))
        {
            sceneManager.GetComponent<ScenesManager>().BringNextSchene();
        }
        //else if (previousScene && playerCollider && Input.GetKeyDown(KeyCode.F))
        //{
        //    sceneManager.ReturnToPreviousScene();
        //}
    }
}
