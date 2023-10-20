using UnityEngine;

namespace Tasks
{
    public interface ITask
    {
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }

        public void StartTask();

        public void CompleteTask();
    }
}