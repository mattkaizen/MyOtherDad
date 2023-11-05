using Data;
using UnityEngine;

namespace Tasks
{
    public class Nap : MonoBehaviour, ITask
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
        
        [SerializeField] private VoidEventChannelData eventToStartNapTask;
        [SerializeField] private VoidEventChannelData eventToCompleteNapTask;
        [Space]
        [SerializeField] private VoidEventChannelData napTaskStarted;
        [SerializeField] private VoidEventChannelData napTaskCompleted;
        
        private bool _isCompleted;
        private bool _isStarted;

        private void OnEnable()
        {
            eventToStartNapTask.EventRaised += OnEventToStartNapTaskRaised;
            eventToCompleteNapTask.EventRaised += OnEventToCompleteNapTaskRaised;
        }

        private void OnDisable()
        {
            eventToStartNapTask.EventRaised -= OnEventToStartNapTaskRaised;
            eventToCompleteNapTask.EventRaised -= OnEventToCompleteNapTaskRaised;
        }

        private void OnEventToStartNapTaskRaised()
        {
            if (IsStarted) return;

            StartTask();
            napTaskStarted.RaiseEvent();
        }

        private void OnEventToCompleteNapTaskRaised()
        {
            if (IsCompleted) return;

            CompleteTask();
            napTaskCompleted.RaiseEvent();
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