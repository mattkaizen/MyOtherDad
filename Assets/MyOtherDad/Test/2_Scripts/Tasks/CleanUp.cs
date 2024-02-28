using System.Collections.Generic;
using Data;
using Objects.UsableInteractable;
using UnityEngine;

namespace Tasks
{
    public class CleanUp : MonoBehaviour, ITask
    {
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public int AmountOfCleanedObjects { get; set; }
        public int AmountOfObjectsToClean => dirtyObjects.Count;

        [Header("Event Channels to listen")]
        [SerializeField] private VoidEventChannelData eventToStartCleanUpTask;
        [SerializeField] private VoidEventChannelData eventToStopCleanUpTask;
        [Space] 
        [Header("Broadcast on Event Channels")]
        [SerializeField] private VoidEventChannelData cleanUpTaskStarted;
        [SerializeField] private VoidEventChannelData cleanUpTaskStopped;
        [SerializeField] private VoidEventChannelData cleanUpTaskCompleted;
        [SerializeField] private IntEventChannelData amountOfCleanedObjectsChanged;
        [Space] 
        [Header("Task Settings")]
        [SerializeField] private List<DirtyObject> dirtyObjects = new List<DirtyObject>();

        private void OnEnable()
        {
            if(eventToStartCleanUpTask != null)
                eventToStartCleanUpTask.EventRaised += OnEventToStartTaskRaised;
            
            if(eventToStopCleanUpTask != null)
                eventToStopCleanUpTask.EventRaised += OnEventToStopTaskRaised;

            foreach (var dirtyObject in dirtyObjects)
            {
                dirtyObject.wasCleaned.AddListener(OnDirtyObjectWasCleaned);
            }
        }

        private void OnDisable()
        {
            if(eventToStartCleanUpTask != null)
                eventToStartCleanUpTask.EventRaised -= OnEventToStartTaskRaised;
            
            if(eventToStopCleanUpTask != null)
                eventToStopCleanUpTask.EventRaised -= OnEventToStopTaskRaised;

            foreach (var dirtyObject in dirtyObjects)
            {
                dirtyObject.wasCleaned.RemoveListener(OnDirtyObjectWasCleaned);
            }
        }

        private void OnDirtyObjectWasCleaned()
        {
            AmountOfCleanedObjects++;
            amountOfCleanedObjectsChanged.RaiseEvent(AmountOfCleanedObjects);

            if (AmountOfCleanedObjects >= AmountOfObjectsToClean)
            {
                CompleteTask();
            }
        }

        private void OnEventToStartTaskRaised()
        {
            StartTask();
        }
        
        private void OnEventToStopTaskRaised()
        {
            StopTask();
        }

        public void StartTask()
        {
            IsStarted = true;
            cleanUpTaskStarted.RaiseEvent();
        }

        public void CompleteTask()
        {
            Debug.Log("CleanUpTask Completed");
            cleanUpTaskCompleted.RaiseEvent();
            IsCompleted = true;
        }

        public void StopTask()
        {
            cleanUpTaskStopped.RaiseEvent();
        }
    }
}