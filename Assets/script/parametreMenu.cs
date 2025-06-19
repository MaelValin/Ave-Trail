using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class parametreMenu : MonoBehaviour
{

     

     public Dropdown resolutionDropdown;
     public Dropdown dropdownLangue;

     Resolution[] resolutions;

     public Slider musicslider;
     public Slider effectslider;

     public AudioMixer audioMixer;

     public static parametreMenu instance;

    private void Awake()
    {
        instance = this;
        
    }

    public void Start()
    {
        
        

        //effectslider.value=volumevalueslidereffect;
        if(dropdownLangue != null)
        {
            dropdownLangue.onValueChanged.AddListener(OnDropdownValueChanged);
            dropdownLangue.value = PlayerPrefs.GetInt("index", 0);
            Debug.Log("index sauvegardée : " + PlayerPrefs.GetInt("index", 0));
            dropdownLangue.RefreshShownValue();
        }
        

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        } 

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;
        
    }

    public void SetQuality(int qualityIndex)
    {
        
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeMusic", volume);
        
        
    }

    public void SetVolumeEffect(float volume)
    {
        audioMixer.SetFloat("volumeEffect", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void MenuButton()
    {
        
        Time.timeScale = 1f;
        

       SceneManager.LoadScene("Menu");
    }

    public void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
        MainMenu.instance.parametreMenu.SetActive(false);
    }


     public void OnDropdownValueChanged(int index)
    {
    var langueManager = LangueManager.instance;

         if (langueManager == null)
    {
        Debug.LogError("langueManager n'est pas assigné !");
        return;
    }
       langueManager.adapteLangue(index);
    }
}
