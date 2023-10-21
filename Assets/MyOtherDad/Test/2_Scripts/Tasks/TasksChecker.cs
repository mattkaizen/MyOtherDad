using System;
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
        [SerializeField] private List<GameObject> tasksPrefabToCheck;

        private List<ITask> _tasksToCheck = new List<ITask>();

        private void Start()
        {
            if (TryGetTasks())
            {
                StartCoroutine(WaitForAllTaskCompletedRoutine());
            }
        }

        private bool TryGetTasks()
        {
            foreach (var taskPrefab in tasksPrefabToCheck)
            {
                if (taskPrefab.TryGetComponent<ITask>(out var task))
                {
                    _tasksToCheck.Add(task);
                }
            }
            
            bool hasTaskToCheck = _tasksToCheck.Count != 0;

            return hasTaskToCheck;
        }

        private bool AreAllTaskCompleted()
        {
            return _tasksToCheck.All(task => task.IsCompleted);
        }

        private IEnumerator WaitForAllTaskCompletedRoutine()
        {
            yield return new WaitUntil(AreAllTaskCompleted);
            allTaskAreCompleted.RaiseEvent();
        }
    }
}