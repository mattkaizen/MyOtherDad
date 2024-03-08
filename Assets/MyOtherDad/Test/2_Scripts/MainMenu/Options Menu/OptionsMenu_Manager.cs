using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsMenu_Manager : MonoBehaviour
{
    public TMP_Dropdown resolutionsDropdown;

    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject credits;

    [SerializeField] Slider sliderAmbient;
    [SerializeField] Slider sliderSFX;

    [SerializeField] AudioMixer masterMixer;

    [SerializeField] Resolution[] resolutions;

    [Range (0, 100)]
    private float ambientVol;
    [Range (0, 100)]
    private float sfxVol;

    public void TurnOffOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void TurnOnCredits()
    {
        credits.SetActive(true);
    }

    public void TurnOffCredits()
    {
        credits.SetActive(false);
    }

    public void AmbientVolume()
    {
        masterMixer.SetFloat("AmbientVolume", Mathf.Log10(sliderAmbient.value) * 20);
    }

    public void SfxVolume()
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(sliderSFX.value) * 20);
    }

    public void ChangeResoltions(int resolutionsIndex)
    {
        PlayerPrefs.SetInt("resolutionNumber", resolutionsDropdown.value); 

        Resolution resolution = resolutions[resolutionsIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void Start()
    {
        sliderAmbient.value = ambientVol;
        sliderSFX.value = sfxVol;

        sliderAmbient.minValue = 0;
        sliderAmbient.maxValue = 100;

        sliderSFX.minValue = 0;
        sliderSFX.maxValue = 100;

        CheckResoltion();
    }

    void Update()
    {
        //AmbientVolume();
        //SfxVolume();
    }

    private void CheckResoltion()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int resolucionActual = 0;

        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i; 
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = resolucionActual;
        resolutionsDropdown.RefreshShownValue();

        resolutionsDropdown.value = PlayerPrefs.GetInt("resolutionNumber", 0); 
    }
}
