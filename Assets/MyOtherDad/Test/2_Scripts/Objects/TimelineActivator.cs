using Data;
using UnityEngine;
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
            
            Debug.Log("Cinematic starting");
            playableDirector.Play();
        }


        private void OnTimelineStopped(PlayableDirector obj)
        {
            timelineStopped.RaiseEvent();
            Debug.Log("Cinematic stopped");
            
        }

        private void OnTimelineStarted(PlayableDirector obj)
        {
            timelineStarted.RaiseEvent();
        }
    }
}