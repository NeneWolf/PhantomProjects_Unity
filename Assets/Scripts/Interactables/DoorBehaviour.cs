using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] int requiredNumberOfKeys;
    [SerializeField] LayerMask whatIsPlayer;

    GameObject sceneManager;
    GameObject gameManager;
    GameObject uiManager;
    GameObject savingManager;

    [SerializeField] Text keycardsTextDisplay;
    //[SerializeField] bool nextScene = false;
    //[SerializeField] bool previousScene = false;

    private void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
        gameManager = GameObject.Find("GameManager");
        uiManager = GameObject.Find("UIManager");

        keycardsTextDisplay.text = requiredNumberOfKeys.ToString();
    }

    void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.x ,whatIsPlayer);

        if (playerCollider && Input.GetKeyDown(KeyCode.F) && uiManager.GetComponent<UIManager>().currentKeycards == requiredNumberOfKeys)
        {
            uiManager.GetComponent<UIManager>().ResetKeyCards();

            if (gameManager.GetComponent<GameManager>().loadedSave == false)
                gameManager.GetComponent<GameManager>().loadedSave = true;

            DataPersistanceManager.instanceData.SaveGame();
            
            sceneManager.GetComponent<ScenesManager>().BringNextScene();
        }

    }
}
