﻿using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Audio
{
    public class AudioSourceController : MonoBehaviour
    {
        public AudioSource AudioSourceToControl => audioSourceToControl;

        [SerializeField] private AudioSource audioSourceToControl;
        [Header("Fade settings")]
        [SerializeField] private float maxVolume = 1;
        [SerializeField] private bool getMaxVolumeFromAudioSource = true;

        private void Awake()
        {
            if (!getMaxVolumeFromAudioSource) return;

            maxVolume = audioSourceToControl.volume;
        }

        [UsedImplicitly]
        public void Play()
        {
            if (audioSourceToControl == null)
            {
                Debug.LogWarning($"Empty {typeof(AudioSource)} ");
                return;
            }
            SetVolume(maxVolume);
            
            audioSourceToControl.Play();
        }

        [UsedImplicitly]
        public void Stop()
        {
            if (audioSourceToControl == null)
            {
                Debug.LogWarning($"Empty {typeof(AudioSource)} ");
                return;
            }

            audioSourceToControl.Stop();
        }

        [UsedImplicitly]
        public void SetVolume(float newVolume)
        {
            if (audioSourceToControl == null)
            {
                Debug.LogWarning($"Empty {typeof(AudioSource)} ");
                return;
            }

            audioSourceToControl.volume = newVolume;
        }

        [UsedImplicitly]
        public void FadeInToMaxVolume(float fadeInDuration)
        {
            if (audioSourceToControl == null)
            {
                Debug.LogWarning($"Empty {typeof(AudioSource)} ");
                return;
            }

            audioSourceToControl.DOFade(maxVolume, fadeInDuration);
        }

        [UsedImplicitly]
        public void FadeOutVolume(float fadeInDuration)
        {
            if (audioSourceToControl == null)
            {
                Debug.LogWarning($"Empty {typeof(AudioSource)} ");
                return;
            }

            audioSourceToControl.DOFade(0.0f, fadeInDuration);
        }

        [UsedImplicitly]
        public void FadeOutVolumeAndStop(float fadeInDuration)
        {
            if (audioSourceToControl == null)
            {
                Debug.LogWarning($"Empty {typeof(AudioSource)} ");
                return;
            }

            audioSourceToControl.DOFade(0.0f, fadeInDuration).OnComplete(Stop);
        }
    }
}