using Data;
using UnityEngine;

namespace Tasks
{
    public class Breakfast : MonoBehaviour, ITask
    {
        [SerializeField] private VoidEventChannelData eventToStartBreakfastTask;
        [SerializeField] private VoidEventChannelData eventToCompleteBreakfastTask;
        [Space]
        [SerializeField] private VoidEventChannelData breakfastTaskStarted;
        [SerializeField] private VoidEventChannelData breakfastTaskCompleted;

        private void OnEnable()
        {
            eventToCompleteBreakfastTask.EventRaised += OnEventToCompleteBreakfastTaskRaised;
            eventToStartBreakfastTask.EventRaised += OnEventToStartBreakfastTaskRaised;
        }

        private void OnDisable()
        {
            eventToCompleteBreakfastTask.EventRaised -= OnEventToCompleteBreakfastTaskRaised;
            eventToStartBreakfastTask.EventRaised -= OnEventToStartBreakfastTaskRaised;
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