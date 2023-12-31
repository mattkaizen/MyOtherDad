﻿using System.Collections;
using System.Collections.Generic;
using CustomInput;
using Data;
using Domain;
using Objects;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    public class ThrowTrash : MonoBehaviour, ITask
    {
        public event UnityAction<bool> HasAllTrashOnHand = delegate { };
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }

        public VoidEventChannelData ThrowTrashTaskStarted => throwTrashTaskStarted;

        public bool IsPreStarted
        {
            get => _isPreStarted;
            set => _isPreStarted = value;
        }

        public VoidEventChannelData ThrowTrashTaskStopped => throwTrashTaskStopped;

        public IntEventChannelData ThrowTrashTaskCompletedWithScoreOf => throwTrashTaskCompletedWithScoreOf;

        [Header("Event Channels to listen")] 
        [SerializeField] private VoidEventChannelData eventToPreStartTask;

        [SerializeField] private VoidEventChannelData eventToStartTask;

        [SerializeField] private VoidEventChannelData eventToStopTask;

        [Space] [Header("Broadcast on Event Channels")] [SerializeField]
        private VoidEventChannelData throwTrashTaskPreStarted;

        [SerializeField] private VoidEventChannelData throwTrashTaskStarted;

        [SerializeField] private VoidEventChannelData throwTrashTaskStopped;
        [SerializeField] private IntEventChannelData throwTrashTaskCompletedWithScoreOf;
        [SerializeField] private VoidEventChannelData enableCameraObjectInputInterrupted;

        [Header("Dependencies")]
        [SerializeField] private TrashDetectorTrigger trashDetectorTrigger;
        [SerializeField] private HandController handController;
        [SerializeField] private PlayerObjectThrower playerObjectThrower;
        [SerializeField] private InputActionControlManagerData inputActionManager;
        [Header("Task settings")]
        [SerializeField] private float newThrowForce = 20.0f;
        [SerializeField] private int amountOfTrashToPick;
        [SerializeField] private List<ItemData> trashDataToCheck;

        private GameObject _lastItemThrown;

        private int _amountOfTrashPicked;
        private bool _isCompleted;
        private bool _isStarted;
        private bool _isPreStarted;

        private void OnEnable()
        {
            eventToStartTask.EventRaised += OnEventToStartTaskRaised;
            eventToStopTask.EventRaised += OnEventToStopTaskRaised;
            eventToPreStartTask.EventRaised += OnEventToPreStartTaskRaised;

            handController.ItemAdded += OnItemAdded;
            handController.ItemRemoved += OnItemRemoved;
        }

        private void OnDisable()
        {
            eventToStartTask.EventRaised -= OnEventToStartTaskRaised;
            eventToStopTask.EventRaised -= OnEventToStopTaskRaised;
            eventToPreStartTask.EventRaised -= OnEventToPreStartTaskRaised;

            handController.ItemAdded -= OnItemAdded;
            handController.ItemRemoved -= OnItemRemoved;
        }

        private void OnEventToPreStartTaskRaised()
        {
            _isPreStarted = true;
            throwTrashTaskPreStarted.RaiseEvent();
        }

        private void OnEventToStartTaskRaised()
        {
            if (IsCompleted) return;
            if (IsStarted) return;

            if (PlayerHasTargetAmountOfTrashOnHand())
                StartTask();
        }

        private void OnEventToStopTaskRaised()
        {
            StopTask();
        }

        public void StartTask()
        {
            enableCameraObjectInputInterrupted.RaiseEvent();
            inputActionManager.GetUpActionControl.DisableInput();
            inputActionManager.LookAtAssetActionControl.EnableInput();
            inputActionManager.ThrowItemActionControl.EnableInput();
            StartCoroutine(CheckIfPlayerThrowAllTrash());

            IsStarted = true;
            playerObjectThrower.SetNewThrowForce(newThrowForce);
            trashDetectorTrigger.IsTrashDetectionEnabled = true;
            throwTrashTaskStarted.RaiseEvent();
            Debug.Log("Throw Trash task started");
        }

        private void StopTask()
        {
            IsStarted = false;
            trashDetectorTrigger.IsTrashDetectionEnabled = false;
            throwTrashTaskStopped.RaiseEvent();
        }

        public void CompleteTask()
        {
            IsCompleted = true;
            inputActionManager.GetUpActionControl.EnableInput();
            playerObjectThrower.SetDefaultThrowForce();
            trashDetectorTrigger.IsTrashDetectionEnabled = false;
            int score = trashDetectorTrigger.AmountObjectAdded;
            throwTrashTaskCompletedWithScoreOf.RaiseEvent(score);
        }

        private void OnItemRemoved(GameObject item)
        {
            if (item.TryGetComponent<IObjectData>(out var objectData))
            {
                if (trashDataToCheck.Contains(objectData.Data))
                {
                    _amountOfTrashPicked--;
                    _lastItemThrown = item;

                    HasAllTrashOnHand?.Invoke(PlayerHasTargetAmountOfTrashOnHand());
                }
            }
        }

        private void OnItemAdded(GameObject item)
        {
            if (item.TryGetComponent<IObjectData>(out var objectData))
            {
                if (trashDataToCheck.Contains(objectData.Data))
                {
                    _amountOfTrashPicked++;

                    HasAllTrashOnHand?.Invoke(PlayerHasTargetAmountOfTrashOnHand());
                }
            }
        }

        private bool PlayerHasTargetAmountOfTrashOnHand()
        {
            return _amountOfTrashPicked >= amountOfTrashToPick;
        }

        private IEnumerator CheckIfPlayerThrowAllTrash()
        {
            yield return new WaitUntil(() => _amountOfTrashPicked == 0);

            yield return new WaitUntil((() =>
            {
                if (_lastItemThrown != null)
                {
                    _lastItemThrown.TryGetComponent<IThrowable>(out var throwable);
                    return
                        throwable.Rigidbody.velocity ==
                        Vector3.zero; //TODO: Tal vez con la velociddad vertical es suficiente
                }
                else
                {
                    return false;
                }
            }));
            CompleteTask();
        }
    }
}