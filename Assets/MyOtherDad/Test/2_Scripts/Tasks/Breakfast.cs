using Data;
using UnityEngine;

namespace Tasks
{
    public class Breakfast : MonoBehaviour, ITask
    {
        [SerializeField] private VoidEventChannelData eventToCompleteBreakfastTask;
        [SerializeField] private VoidEventChannelData eventToStartBreakfastTask;
        [SerializeField] private VoidEventChannelData eventToStopBreakfastTask;
        [Space]
        [SerializeField] private VoidEventChannelData breakfastTaskCompleted;
        [SerializeField] private VoidEventChannelData breakfastTaskStarted;
        [SerializeField] private VoidEventChannelData breakfastTaskStopped;

        private void OnEnable()
        {
            eventToCompleteBreakfastTask.EventRaised += OnEventToCompleteBreakfastTaskRaised;
            eventToStartBreakfastTask.EventRaised += OnEventToStartBreakfastTaskRaised;
            eventToStopBreakfastTask.EventRaised += OnEventToStopBreakfastTaskRaised;
        }

        private void OnDisable()
        {
            eventToCompleteBreakfastTask.EventRaised -= OnEventToCompleteBreakfastTaskRaised;
            eventToStartBreakfastTask.EventRaised -= OnEventToStartBreakfastTaskRaised;
            eventToStopBreakfastTask.EventRaised -= OnEventToStopBreakfastTaskRaised;
        }

        private void OnEventToStopBreakfastTaskRaised()
        {
            TryStopTask();
        }

        private void OnEventToStartBreakfastTaskRaised()
        {
            if (IsStarted) return;

            StartTask();
            breakfastTaskStarted.RaiseEvent();
        }

        private void OnEventToCompleteBreakfastTaskRaised()
        {
            if (IsCompleted) return;

            CompleteTask();
            breakfastTaskCompleted.RaiseEvent();
        }

        private void TryStopTask()
        {
            if (IsCompleted) return;

            IsStarted = false;
            breakfastTaskStopped.RaiseEvent();
        }

        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public void StartTask()
        {
            throw new System.NotImplementedException();
        }

        public void CompleteTask()
        {
            throw new System.NotImplementedException();
        }
    }
}