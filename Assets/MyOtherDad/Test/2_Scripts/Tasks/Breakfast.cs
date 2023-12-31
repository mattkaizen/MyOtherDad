﻿using Data;
using UnityEngine;

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
        
        [SerializeField] private VoidEventChannelData eventToCompleteBreakfastTask;
        [Space]
        [SerializeField] private VoidEventChannelData breakfastTaskCompleted;

        private bool _isStarted;
        private bool _isCompleted;

        private void OnEnable()
        {
            eventToCompleteBreakfastTask.EventRaised += OnEventToCompleteBreakfastTaskRaised;
        }

        private void OnDisable()
        {
            eventToCompleteBreakfastTask.EventRaised -= OnEventToCompleteBreakfastTaskRaised;
        }

        private void OnEventToCompleteBreakfastTaskRaised()
        {
            if (IsCompleted) return;

            CompleteTask();
            breakfastTaskCompleted.RaiseEvent();
        }
        public void StartTask()
        {
            IsStarted = true;
        }

        public void CompleteTask()
        {
            IsCompleted = true;
        }
    }
}