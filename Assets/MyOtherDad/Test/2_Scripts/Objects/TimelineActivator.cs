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
            eventToPlayTimeline.EventRaised += OnEventToPlayCinematicRaised;
            playableDirector.played += OnTimelineStarted;
            playableDirector.stopped += OnTimelineStopped;
        }

        private void OnDisable()
        {
            eventToPlayTimeline.EventRaised -= OnEventToPlayCinematicRaised;
            playableDirector.played -= OnTimelineStarted;
            playableDirector.stopped -= OnTimelineStopped;
        }

        private void OnEventToPlayCinematicRaised()
        {
            if (playableDirector.state == PlayState.Playing) return;
            
            playableDirector.Play();
            cinematicStarted?.Invoke();
            Debug.Log("Cinematic starting");
        }


        private void OnTimelineStopped(PlayableDirector obj)
        {
            timelineStopped.RaiseEvent();
            cinematicStopped?.Invoke();

            Debug.Log("Cinematic stopped");

            if (disableCinematicWhenStops)
                this.enabled = false;
            
            Debug.Log("Timeline Activator disabled");
            
        }

        private void OnTimelineStarted(PlayableDirector obj)
        {
            timelineStarted.RaiseEvent();
        }
    }
}