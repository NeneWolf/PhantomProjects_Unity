using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsCanvas : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    public AudioMixer[] audioMixer;
    Resolution[] resolutions;

    public Slider[] sliders;
    
    public Dropdown resolutionDropdown;
    private void Start()
    {
        sliders[0].value = GameObject.FindObjectOfType<UIManager>().gameObject.GetComponent<UIManager>().effectVolume;
        sliders[1].value = GameObject.FindObjectOfType<UIManager>().gameObject.GetComponent<UIManager>().backgroundVolume;
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;


        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }


    public void TurnOnControls()
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(true);
    }

    public void TurnOnSettings()
    {
        buttons[0].SetActive(true);
        buttons[1].SetActive(false);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer[0].SetFloat("Effects", volume);
        GameObject.FindObjectOfType<UIManager>().gameObject.GetComponent<UIManager>().effectVolume = volume;
    }

    public void SetBackgroundVolume(float volume)
    {
        audioMixer[1].SetFloat("Background", volume);
        GameObject.FindObjectOfType<UIManager>().gameObject.GetComponent<UIManager>().backgroundVolume = volume;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
