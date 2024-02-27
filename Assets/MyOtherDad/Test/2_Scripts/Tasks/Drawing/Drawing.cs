using CustomInput;
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

        [Header("Listen to Event Channels")]
        [SerializeField] private VoidEventChannelData eventToCompleteDrawingTask;
        [SerializeField] private VoidEventChannelData eventToStartDrawingTask;
        [SerializeField] private VoidEventChannelData eventToStopDrawingTask;
        [Space]
        [Header("Broadcast on Event Channels")]
        [SerializeField] private VoidEventChannelData drawingTaskCompleted;
        [SerializeField] private VoidEventChannelData drawingTaskStarted;
        [SerializeField] private VoidEventChannelData drawingTaskStopped;
        [SerializeField] private VoidEventChannelData enableCameraObjectInputInterrupted;
        [Space]
        [Header("Dependencies")]
        [SerializeField] private InputActionControlManagerData inputActionManager;

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
            enableCameraObjectInputInterrupted.RaiseEvent();
            inputActionManager.GetUpActionControl.DisableInput();
            inputActionManager.PaintActionControl.EnableInput();
        }

        public void CompleteTask()
        {
            Debug.Log($"Drawing mini game Completed: {gameObject.name}");
            IsCompleted = true;

            inputActionManager.GetUpActionControl.EnableInput();
            inputActionManager.PaintActionControl.DisableInput();
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