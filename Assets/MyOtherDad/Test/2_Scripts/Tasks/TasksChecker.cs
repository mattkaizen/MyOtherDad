using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Tasks
{
    public class TasksChecker : MonoBehaviour
    {
        [SerializeField] private List<Task> tasksToCheck;
        [SerializeField] private VoidEventChannelData allTaskAreCompleted;

        private bool AreAllTaskCompleted()
        {
            return tasksToCheck.All(task => task.IsCompleted);
        }

        private IEnumerator WaitForAllTaskCompleted()
        {
            yield return new WaitUntil(AreAllTaskCompleted);
            allTaskAreCompleted.RaiseEvent();
        }

    }
}