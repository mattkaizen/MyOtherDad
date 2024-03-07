using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu_Manager : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject credits;

    [SerializeField] Slider sliderAmbient;
    [SerializeField] Slider sliderSFX;

    [SerializeField] AudioMixer masterMixer;

    [Range (-20, 20)]
    private float ambientVol;
    [Range (-20, 20)]
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

    void Start()
    {
        sliderAmbient.value = ambientVol;
        sliderSFX.value = sfxVol;

        sliderAmbient.minValue = -100;
        sliderAmbient.maxValue = 100;

        sliderSFX.minValue = -100;
        sliderSFX.maxValue = 100;
    }

    void Update()
    {
        AmbientVolume();
    }

    public void AmbientVolume()
    {
        masterMixer.SetFloat("AmbientVolume", Mathf.Log10(sliderAmbient.value) * 20);
    }

    public void SfxVolume() 
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(sliderSFX.value) * 20); 
    }
}
