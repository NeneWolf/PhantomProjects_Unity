using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] int requiredNumberOfKeys;
    [SerializeField] LayerMask whatIsPlayer;

    GameObject sceneManager;
    GameObject gameManager;
    GameObject uiManager;
    GameObject savingManager;

    [SerializeField] bool nextScene = false;
    [SerializeField] bool previousScene = false;

    private void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
        savingManager = GameObject.Find("SavingManager");
        gameManager = GameObject.Find("GameManager");
        uiManager = GameObject.Find("UIManager");
    }

    void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.x ,whatIsPlayer);

        if (playerCollider && Input.GetKeyDown(KeyCode.F) && uiManager.GetComponent<UIManager>().currentKeycards == requiredNumberOfKeys)
        {
            savingManager.GetComponent<DataPersistanceManager>().SaveGame();
            sceneManager.GetComponent<ScenesManager>().BringNextSchene();
        }
        //else if (previousScene && playerCollider && Input.GetKeyDown(KeyCode.F))
        //{
        //    sceneManager.ReturnToPreviousScene();
        //}
    }
}
