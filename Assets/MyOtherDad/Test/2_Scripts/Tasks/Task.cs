using UnityEngine;

namespace Tasks
{
    public abstract class Task : MonoBehaviour
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

        private bool _isCompleted;
        private bool _isStarted;

        protected void StartTask()
        {
            _isStarted = true;
        }

        protected void CompleteTask()
        {
            IsCompleted = true;
        }
    }
}