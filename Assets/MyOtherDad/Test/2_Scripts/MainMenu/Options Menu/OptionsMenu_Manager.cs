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

    [SerializeField] AudioMixerGroup ambientMixer;
    [SerializeField] AudioMixerGroup sfxMixer;

    private float ambientVol;
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
        
    }
}
