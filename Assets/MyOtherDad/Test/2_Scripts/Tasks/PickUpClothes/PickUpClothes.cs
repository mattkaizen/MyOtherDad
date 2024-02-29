using Data;
using Objects.Clothes;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    public class PickUpClothes : MonoBehaviour, ITask
    {
        public VoidEventChannelData PickUpClothesTaskStarted => pickUpClothesTaskStarted;

        public VoidEventChannelData PickUpClothesTaskCompleted => pickUpClothesTaskCompleted;
        public int AmountOfPickedClothes => clothesContainer.AmountOfClothesPicked;
        public int RequiredAmountOfClothesToPick => requiredAmountOfClothesToPick;
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        

        [SerializeField] private UnityEvent taskCompleted;
        [Space] 
        [Header("Event Channels to listen")] 
        [SerializeField] private VoidEventChannelData eventToStartTask;
        [SerializeField] private VoidEventChannelData eventToCompleteTask;
        [SerializeField] private VoidEventChannelData eventToStopTask;
        [Space] 
        [Header("Broadcast on Event Channels")] 
        [SerializeField] private VoidEventChannelData pickUpClothesTaskStarted;
        [SerializeField] private VoidEventChannelData pickUpClothesTaskStopped;
        [SerializeField] private VoidEventChannelData pickUpClothesTaskCompleted;
        [Space] 
        [Header("Task Settings")] 
        [SerializeField] private int requiredAmountOfClothesToPick;
        [SerializeField] private UsableClothesContainer clothesContainer;

        private void OnEnable()
        {
            if (eventToStartTask != null)
                eventToStartTask.EventRaised += OnEventToStartTaskRaised;
            
            if (eventToStopTask != null)
                eventToStopTask.EventRaised += OnEventToStopTaskRaised;

            if (eventToCompleteTask != null)
                eventToCompleteTask.EventRaised += OnEventToCompleteTaskRaised;

        }


        private void OnDisable()
        {
            if (eventToStartTask != null)
                eventToStartTask.EventRaised -= OnEventToStartTaskRaised;
            
            if (eventToStopTask != null)
                eventToStopTask.EventRaised -= OnEventToStopTaskRaised;

            if (eventToCompleteTask != null)
                eventToCompleteTask.EventRaised -= OnEventToCompleteTaskRaised;
            

        }
        
        private void OnEventToCompleteTaskRaised()
        {
            CompleteTask();
        }
        private void OnEventToStartTaskRaised()
        {
            StartTask();
        }

        private void OnEventToStopTaskRaised()
        {
            if (pickUpClothesTaskStopped != null)
                pickUpClothesTaskStopped.RaiseEvent();
        }

        public void StartTask()
        {
            Debug.Log("Task Clothes: Started");

            IsStarted = true;
            pickUpClothesTaskStarted.RaiseEvent();
        }

        public void CompleteTask()
        {
            if (!IsStarted) return;
            
            IsCompleted = true;
            
            Debug.Log("Task Clothes: Completed");
            pickUpClothesTaskCompleted.RaiseEvent();
            taskCompleted?.Invoke();
        }
    }
}