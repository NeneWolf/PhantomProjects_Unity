using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    float fadeinTime = 2f;
    float fadeoutTime = 0.5f;
    bool isFading = false;

    CanvasGroup canvasGroup;

    private void Start()
    {
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

