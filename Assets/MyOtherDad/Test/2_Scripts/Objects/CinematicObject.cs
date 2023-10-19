using Data;
using Interfaces;
using UnityEngine;
using UnityEngine.Playables;

namespace Objects
{
    public class CinematicObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private VoidEventChannelData timelineStartedEvent;
        [SerializeField] private VoidEventChannelData timelineStoppedEvent;

        [SerializeField] private bool canBeReUsed;

        private void Awake()
        {
            playableDirector.played += OnTimelineStarted;
            playableDirector.stopped += OnTimelineStopped;
        }

        private void OnTimelineStopped(PlayableDirector obj)
        {
            timelineStoppedEvent.RaiseEvent();
        }

        private void OnTimelineStarted(PlayableDirector obj)
        {
            timelineStartedEvent.RaiseEvent();
        }

        public void Interact()
        {
            if (!canBeReUsed) return;
            
            canBeReUsed = false;
            playableDirector.Play();
        }
    }
}