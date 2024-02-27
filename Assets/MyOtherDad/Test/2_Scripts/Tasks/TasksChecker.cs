using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    public class TasksChecker : MonoBehaviour
    {
        [Header("Broadcast on Events")]
        [SerializeField] private VoidEventChannelData allTaskAreCompleted;
        [Space][UsedImplicitly]
        [SerializeField] private UnityEvent allTaskAreCompletedEvent;
        [Space]
        [SerializeField] private List<GameObject> principalTasksPrefabToCheck;
        [SerializeField] private List<GameObject> secondaryTasksPrefabToCheck;

        private List<ITask> _principalTasksToCheck = new List<ITask>();
        private List<ITask> _secondaryTasksToCheck = new List<ITask>();

        private void Awake()
        {
            TryGetTasks();
        }

        private void Start()
        {
            if (HaveTasksToCheck())
            {
                StartCoroutine(WaitForAllTaskCompletedRoutine());
            }
        }

        private void TryGetTasks()
        {
            foreach (var taskPrefab in principalTasksPrefabToCheck)
            {
                if (taskPrefab.TryGetComponent<ITask>(out var task))
                {
                    _principalTasksToCheck.Add(task);
                }
            }

            foreach (var taskPrefab in secondaryTasksPrefabToCheck)
            {
                if (taskPrefab.TryGetComponent<ITask>(out var task))
                {
                    _secondaryTasksToCheck.Add(task);
                }
            }
        }

        private bool HaveTasksToCheck()
        {
            return _principalTasksToCheck.Count != 0;
        }

        private bool AreAllTaskCompleted()
        {
            return _principalTasksToCheck.All(task => task.IsCompleted);
        }

        private IEnumerator WaitForAllTaskCompletedRoutine()
        {
            yield return new WaitUntil(AreAllTaskCompleted);

            Debug.Log("All task are completed");
            foreach (var secondaryTask in _secondaryTasksToCheck)
            {
                if (secondaryTask.IsStarted)
                {
                    yield return new WaitUntil((() => secondaryTask.IsCompleted));
                }
            }

            if (allTaskAreCompleted != null)
                allTaskAreCompleted.RaiseEvent();
         
            allTaskAreCompletedEvent?.Invoke();
        }
    }
}