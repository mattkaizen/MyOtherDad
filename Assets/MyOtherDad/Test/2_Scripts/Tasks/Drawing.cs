using Data;
using UnityEngine;

namespace Tasks
{
    public class Drawing : Task
    {
        [SerializeField] private VoidEventChannelData eventToCompleteDrawingTask;
        [SerializeField] private VoidEventChannelData eventToStartDrawingTask;
        [SerializeField] private VoidEventChannelData drawingTaskCompleted;
        [SerializeField] private VoidEventChannelData drawingTaskStarted;

        private void OnEnable()
        {
            eventToCompleteDrawingTask.EventRaised += OnEventToCompleteDrawingTaskRaised;
            eventToStartDrawingTask.EventRaised += OnEventToStartDrawingTaskRaised;
        }

        private void OnEventToStartDrawingTaskRaised()
        {
            if (IsStarted) return;

            StartTask();
            drawingTaskStarted.RaiseEvent();
        }

        private void OnEventToCompleteDrawingTaskRaised()
        {
            if (IsCompleted) return;

            CompleteTask();
            drawingTaskCompleted.RaiseEvent();
        }
    }
}