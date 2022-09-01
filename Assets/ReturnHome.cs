using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnHome : MonoBehaviour
{
    GameObject SceneManager;
    
    public void ReturnToHome()
    {
        SceneManager = GameObject.FindObjectOfType<ScenesManager>().gameObject;
        SceneManager.GetComponent<ScenesManager>().LoadScene(0);
    }
}
