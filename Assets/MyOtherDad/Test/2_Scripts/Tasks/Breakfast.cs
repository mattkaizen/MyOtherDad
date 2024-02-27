using Data;
using Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    public class Breakfast : MonoBehaviour, ITask
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
        
        [Header("Listen to Events Channels")]
        [Header("Broadcast On Events Channels")]
        [SerializeField] private VoidEventChannelData breakfastTaskCompleted;
        [Space]
        [SerializeField] private UnityEvent breakfastTaskCompletedTask;
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
            breakfastTaskCompleted.RaiseEvent();
            breakfastTaskCompletedTask?.Invoke();
        }
        public void StartTask()
        {
            IsStarted = true;
        }

        public void CompleteTask()
        {
            Debug.Log("Breakfast task Completed");
            IsCompleted = true;
        }
    }
}