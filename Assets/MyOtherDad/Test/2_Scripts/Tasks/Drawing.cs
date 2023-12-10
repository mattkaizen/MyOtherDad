using System;
using Data;
using UnityEngine;

namespace Tasks
{
    public class Drawing : MonoBehaviour, ITask
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

        [SerializeField] private VoidEventChannelData eventToCompleteDrawingTask;
        [SerializeField] private VoidEventChannelData eventToStartDrawingTask;
        [SerializeField] private VoidEventChannelData eventToStopDrawingTask;
        [Space] [SerializeField] private VoidEventChannelData drawingTaskCompleted;
        [SerializeField] private VoidEventChannelData drawingTaskStarted;
        [SerializeField] private VoidEventChannelData drawingTaskStopped;

        private bool _isCompleted;
        private bool _isStarted;

        private void OnEnable()
        {
            eventToCompleteDrawingTask.EventRaised += OnEventToCompleteDrawingTaskRaised;
            eventToStartDrawingTask.EventRaised += OnEventToStartDrawingTaskRaised;
            eventToStopDrawingTask.EventRaised += OnEventToStopDrawingTaskRaised;
        }

        private void OnDisable()
        {
            eventToCompleteDrawingTask.EventRaised -= OnEventToCompleteDrawingTaskRaised;
            eventToStartDrawingTask.EventRaised -= OnEventToStartDrawingTaskRaised;
            eventToStopDrawingTask.EventRaised -= OnEventToStopDrawingTaskRaised;
        }

        public void StartTask()
        {
            IsStarted = true;
        }

        public void CompleteTask()
        {
            IsCompleted = true;
        }

        private void OnEventToStopDrawingTaskRaised()
        {
            TryStopTask();
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

        private void TryStopTask()
        {
            if (IsCompleted) return;

            IsStarted = false;
            drawingTaskStopped.RaiseEvent();
        }
    }
}