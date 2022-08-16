using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour
{

    [SerializeField] int requiredNumberOfKeys;
    [SerializeField] LayerMask whatIsPlayer;

    ScenesManager sceneManager;
    GameManager gameManager;
    UIManager uiManager;

    [SerializeField] bool nextScene = false;
    [SerializeField] bool previousScene = false;

    private void Awake()
    {
        sceneManager = GetComponent<ScenesManager>();
        gameManager = GetComponent<GameManager>();
        uiManager = GetComponent<UIManager>();
    }

    void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.x ,whatIsPlayer);

        if (nextScene && playerCollider && Input.GetKeyDown(KeyCode.F) && uiManager.currentKeycards == requiredNumberOfKeys)
        {
            sceneManager.BringNextSchene();
        }
        else if (previousScene && playerCollider && Input.GetKeyDown(KeyCode.F))
        {
            sceneManager.ReturnToPreviousScene();
        }
    }
}
