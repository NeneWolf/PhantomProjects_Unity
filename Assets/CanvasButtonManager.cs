using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;

public class CanvasButtonManager : MonoBehaviour
{
    GameObject sceneManager;
    GameObject savingManager;

    // 0 - Main Menu //1 - Select Slot // 2 - Mode
    [SerializeField] GameObject[] menus;

    //Background Effect
    [SerializeField] GameObject pCapsule;

    int difficultyLevel;

    void Start()
    {
        sceneManager = GameObject.FindObjectOfType<ScenesManager>().gameObject;
        savingManager = GameObject.Find("SavingManager");
    }

    public void MoveToSelectMenu()
    {
        menus[1].SetActive(true);
        menus[0].SetActive(false);
        pCapsule.GetComponent<Capsule>().TurnOnFog();
    }

    public void MoveToModeMenu()
    {
        menus[2].SetActive(true);
        menus[1].SetActive(false);
    }
    
    public void ReturnToMainMenu()
    {
        pCapsule.GetComponent<Capsule>().TurnOffFog();

        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }

        menus[0].SetActive(true);
    }

    //Set difficulty
    public void DisableDifficultyMenu(int difficultyLevel)
    {
        this.difficultyLevel = difficultyLevel;
        menus[2].SetActive(false);
        StartCoroutine(TheBreak());
    }

    void DifficultySelected(int difficultyLevel)
    {
        FindObjectOfType<DifficultyManager>().SetDifficulty(difficultyLevel);
        FindObjectOfType<GameManager>().gameDifficulty = difficultyLevel;
        sceneManager.GetComponent<ScenesManager>().BringNextScene("Character Selection");
    }

    IEnumerator TheBreak()
    {
        FindObjectOfType<Capsule>().PlayAnimationCrack();
        yield return new WaitForSeconds(2f);
        DifficultySelected(difficultyLevel);
    }

    public void LoadSavedScene()
    {
        sceneManager.GetComponent<ScenesManager>().LoadScene(savingManager.GetComponent<DataPersistanceManager>().lastLevelIndex);
    }
    
    public void MoveToScene(string name)
    {
        sceneManager.GetComponent<ScenesManager>().BringNextScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
