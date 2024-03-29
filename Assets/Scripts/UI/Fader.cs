using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public bool isLoading;
    GameObject m_audioManager;
    [SerializeField] GameObject Loading;
    
    float fadeinTime = 2f;
    float fadeoutTime = 0.5f;
    bool isFading = false;

    CanvasGroup canvasGroup;

    private void Start()
    {
        m_audioManager = FindObjectOfType<AudioManager>().gameObject;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        StartCoroutine(FadeOutIn());
    }

    IEnumerator FadeOutIn()
    {
        yield return FadeIn(fadeinTime);
    }

    IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    IEnumerator FadeIn(float time)
    {
        if (SceneManager.GetActiveScene().buildIndex >= 6)
        {
            Time.timeScale = 0f;
            Loading.SetActive(true);
            isLoading = true;
            m_audioManager.gameObject.GetComponent<AudioManager>().StopAllSounds();
            yield return new WaitForSecondsRealtime(5f);
            
            Time.timeScale = 1f;
            isLoading = false;
            Loading.SetActive(false);
        }
            
        
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }

        this.gameObject.SetActive(false);
    }

    public void FadeOutOn()
    {
        StartCoroutine(FadeOut(fadeoutTime));
    }

    public void FadeInOn()
    {
        StartCoroutine(FadeOut(fadeinTime));
    }

    public bool StatusFade()
    {
        return isFading;
    }
}

