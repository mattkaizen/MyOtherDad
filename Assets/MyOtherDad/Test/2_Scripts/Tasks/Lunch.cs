using Data;
using Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    public class Lunch : MonoBehaviour, ITask
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
        
        [SerializeField] private UnityEvent taskCompleted;
        [Space]
        [Header("Listen to Events Channels")]
        [Header("Broadcast On Events Channels")]
        [SerializeField] private VoidEventChannelData lunchTaskCompleted;
        [Space]
        [Header("Breakfast settings")]
        [SerializeField] private ItemContainer ghostPickableTray;

        private bool _isStarted;
        private bool _isCompleted;

        private void OnEnable()
        {
            ghostPickableTray.ItemPlaced.AddListener(ItemPlaced);
        }
        private void OnDisable()
        {
            ghostPickableTray.ItemPlaced.RemoveListener(ItemPlaced);
        }
        private void ItemPlaced()
        {
            if (IsCompleted) return;

            CompleteTask();
        }
        public void StartTask()
        {
            IsStarted = true;
        }

        public void CompleteTask()
        {
            Debug.Log("Dinner task Completed");
            IsCompleted = true;
            lunchTaskCompleted.RaiseEvent();
            taskCompleted?.Invoke();
        }
    }
    
    
    public class Dinner : MonoBehaviour, ITask
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
        
        [SerializeField] private UnityEvent taskCompleted;
        [Space]
        [Header("Listen to Events Channels")]
        [Header("Broadcast On Events Channels")]
        [SerializeField] private VoidEventChannelData dinnerTaskCompleted;
        [Space]
        [Header("Breakfast settings")]
        [SerializeField] private ItemContainer ghostPickableTray;

        private bool _isStarted;
        private bool _isCompleted;

        private void OnEnable()
        {
            ghostPickableTray.ItemPlaced.AddListener(ItemPlaced);
        }
        private void OnDisable()
        {
            ghostPickableTray.ItemPlaced.RemoveListener(ItemPlaced);
        }
        private void ItemPlaced()
        {
            if (IsCompleted) return;

            CompleteTask();
        }
        public void StartTask()
        {
            IsStarted = true;
        }

        public void CompleteTask()
        {
            Debug.Log("Dinner task Completed");
            IsCompleted = true;
            dinnerTaskCompleted.RaiseEvent();
            taskCompleted?.Invoke();
        }
    }
}