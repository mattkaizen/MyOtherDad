using Data;
using UnityEngine;

namespace Tasks
{
    public class Sleeping : MonoBehaviour, ITask
    {
        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }

        public bool IsStarted
        {
            get => _isStarted;
            set => _isStarted = value;
        }
        
        [SerializeField] private VoidEventChannelData eventToStartSleepingTask;
        [SerializeField] private VoidEventChannelData eventToCompleteSleepingTask;
        [Space]
        [SerializeField] private VoidEventChannelData sleepingTaskStarted;
        [SerializeField] private VoidEventChannelData sleepingTaskCompleted;
        
        private bool _isCompleted;
        private bool _isStarted;

        private void OnEnable()
        {
            eventToStartSleepingTask.EventRaised += OnEventToStartSleepingTaskRaised;
            eventToCompleteSleepingTask.EventRaised += OnEventToCompleteSleepingTaskRaised;
        }

        private void OnDisable()
        {
            eventToStartSleepingTask.EventRaised -= OnEventToStartSleepingTaskRaised;
            eventToCompleteSleepingTask.EventRaised -= OnEventToCompleteSleepingTaskRaised;
        }

        private void OnEventToStartSleepingTaskRaised()
        {
            if (IsStarted) return;

            StartTask();
            sleepingTaskStarted.RaiseEvent();
        }

        private void OnEventToCompleteSleepingTaskRaised()
        {
            if (IsCompleted) return;

            CompleteTask();
            sleepingTaskCompleted.RaiseEvent();
        }
        

        public void StartTask()
        {
            _isStarted = true;
        }

        public void CompleteTask()
        {
            _isCompleted = true;
        }
    }
}