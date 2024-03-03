using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Audio
{
    public class MultipleAudioSourceController : MonoBehaviour
    {
        [SerializeField]
        private List<AudioSourceController> audioSourcesControllers = new List<AudioSourceController>();
        [SerializeField] private bool disableInactiveAudioSourcesOnAwake = true;
        [SerializeField] private bool disableIfLastAudioSourceIsNotPlaying = true;

        private int _currentIndex;

        private void Awake()
        {
            if (!disableInactiveAudioSourcesOnAwake) return;

            for (int i = 0; i < audioSourcesControllers.Count - 1; i++)
            {
                if (i == 0)
                    continue;

                audioSourcesControllers[i].AudioSourceToControl.enabled = false;
            }
        }

        [UsedImplicitly]
        public void PlayByListOrder()
        {
            if (audioSourcesControllers.Count == 0) return;
            if (_currentIndex == audioSourcesControllers.Count) return;

            audioSourcesControllers[_currentIndex].AudioSourceToControl.enabled = true;
            audioSourcesControllers[_currentIndex].Play();

            if (disableIfLastAudioSourceIsNotPlaying)
                StartCoroutine(DisableAudioSourceWhenIsNotPlayingRoutine(audioSourcesControllers[_currentIndex]));

            TryAddNewCurrentIndex();
        }

        private IEnumerator DisableAudioSourceWhenIsNotPlayingRoutine(AudioSourceController controller)
        {
            yield return new WaitForSeconds(controller.AudioSourceToControl.clip.length);
            controller.AudioSourceToControl.enabled = false;
        }

        [UsedImplicitly]
        public void StopCurrentSound()
        {
            if (audioSourcesControllers.Count == 0) return;
            if (audioSourcesControllers[_currentIndex] == null) return;

            audioSourcesControllers[_currentIndex].Stop();
        }

        [UsedImplicitly]
        public void FadeOutCurrentSound(float duration)
        {
            if (audioSourcesControllers.Count == 0) return;
            if (audioSourcesControllers[_currentIndex] == null) return;

            audioSourcesControllers[_currentIndex].FadeOutVolume(duration);
        }

        [UsedImplicitly]
        public void ResetCurrentIndex()
        {
            _currentIndex = 0;
        }

        private void TryAddNewCurrentIndex()
        {
            if (_currentIndex == audioSourcesControllers.Count - 1)
                _currentIndex = 0;
            else
            {
                _currentIndex++;
            }
        }
    }
}