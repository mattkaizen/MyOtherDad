using UnityEngine;

namespace Tasks
{
    public class TaskDebugger : MonoBehaviour, ITask
    {
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }

        private void Start()
        {
            Invoke("StartTask", 1.0f);
            Invoke("CompleteTask", 3.0f);
        }

        public void StartTask()
        {
            Debug.Log($"Start Task: is {gameObject.name}");
            Debug.Log($"Task State: {IsCompleted} is {gameObject.name}");
        }

        public void CompleteTask()
        {
            IsCompleted = true;
            Debug.Log($"Task State: {IsCompleted} is {gameObject.name}");

        }
    }
}