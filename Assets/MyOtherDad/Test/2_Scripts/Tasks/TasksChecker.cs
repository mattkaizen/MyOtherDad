using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Tasks
{
    public class TasksChecker : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelData allTaskAreCompleted;
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
            if(HaveTasksToCheck())
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

            foreach (var secondaryTask in _secondaryTasksToCheck)
            {
                if (secondaryTask.IsStarted)
                {
                    yield return new WaitUntil((() => secondaryTask.IsCompleted));
                }
            }
            allTaskAreCompleted.RaiseEvent();
        }
    }
}