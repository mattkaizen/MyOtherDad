using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Objects
{
    public class TimelineActivator : MonoBehaviour
    {
        [SerializeField] private PlayableDirector playableDirector;
        [Header("Events to Listen")]
        [SerializeField] private VoidEventChannelData eventToPlayTimeline;
        [Header("Broadcast on Event Channels")]
        [SerializeField] private VoidEventChannelData timelineStarted;
        [SerializeField] private VoidEventChannelData timelineStopped;
        [Space]
        [SerializeField] private UnityEvent cinematicStarted;
        [SerializeField] private UnityEvent cinematicStopped;
        [SerializeField] private bool disableCinematicWhenStops;

        private void OnEnable()
        {
            if (eventToPlayTimeline != null)
                eventToPlayTimeline.EventRaised += OnEventToPlayCinematicRaised;

            playableDirector.played += OnTimelineStarted;
            playableDirector.stopped += OnTimelineStopped;
        }

        private void OnDisable()
        {
            if (eventToPlayTimeline != null)
                eventToPlayTimeline.EventRaised -= OnEventToPlayCinematicRaised;

            playableDirector.played -= OnTimelineStarted;
            playableDirector.stopped -= OnTimelineStopped;
        }

        private void OnEventToPlayCinematicRaised()
        {
            if (playableDirector.state == PlayState.Playing) return;

            PlayTimeLine();
        }

        public void PlayTimeLine()
        {
            playableDirector.Play();
            cinematicStarted?.Invoke();
        }

        private void OnTimelineStopped(PlayableDirector obj)
        {
            if (timelineStopped != null)
                timelineStopped.RaiseEvent();
            
            cinematicStopped?.Invoke();

            if (disableCinematicWhenStops)
                this.enabled = false;
        }

        private void OnTimelineStarted(PlayableDirector obj)
        {
            if (timelineStarted == null) return;

            timelineStarted.RaiseEvent();
        }
    }
}