using Data;
using Objects;
using UnityEngine;

namespace Tasks
{
    public class TakePills : MonoBehaviour, ITask
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
        
        [SerializeField] private VoidEventChannelData eventToCompleteTakePill;
        [Space]
        [SerializeField] private VoidEventChannelData takePillTaskCompleted;

        [SerializeField] private PickableObject pickableObject;
        
        private bool _isCompleted;
        private bool _isStarted;

        private void OnEnable()
        {
            eventToCompleteTakePill.EventRaised += OnEventToCompleteSleepingTaskRaised;
        }

        private void OnDisable()
        {
            eventToCompleteTakePill.EventRaised -= OnEventToCompleteSleepingTaskRaised;
        }
        private void OnEventToCompleteSleepingTaskRaised()
        {
            if (IsCompleted) return;

            CompleteTask();
            takePillTaskCompleted.RaiseEvent();
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