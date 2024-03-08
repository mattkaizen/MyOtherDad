using System;
using System.Collections;
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
        public VoidEventChannelData ThrowTrashTaskStopped => throwTrashTaskStopped;
        public IntEventChannelData ThrowTrashTaskCompletedWithScoreOf => throwTrashTaskCompletedWithScoreOf;
        public VoidEventChannelData ThrowTrashTaskPreStarted => throwTrashTaskPreStarted;
        public int MaxAmountOfTrashToScore => amountOfTrashToPick;


        [Header("Event Channels to listen")]
        [SerializeField] private VoidEventChannelData eventToPreStartTask;
        [SerializeField] private VoidEventChannelData eventToStartTask;
        [SerializeField] private VoidEventChannelData eventToStopTask;
        [Space]
        [Header("Broadcast on Event Channels")]
        [SerializeField] private VoidEventChannelData throwTrashTaskPreStarted;
        [SerializeField] private VoidEventChannelData throwTrashTaskStarted;
        [SerializeField] private VoidEventChannelData throwTrashTaskStopped;
        [SerializeField] private IntEventChannelData throwTrashTaskCompletedWithScoreOf;
        [SerializeField] private VoidEventChannelData enableCameraObjectInputInterrupted;
        [Space]
        [Header("Dependencies")]
        [SerializeField] private TrashDetectorTrigger trashDetectorTrigger;
        [SerializeField] private HandController handController;
        [SerializeField] private PlayerObjectThrower playerObjectThrower;
        [SerializeField] private InputActionControlManagerData inputActionManager;
        [Space]
        [Header("Task settings")]
        [SerializeField] private float newThrowForce = 20.0f;
        [SerializeField] private int amountOfTrashToPick;
        [SerializeField] private List<ItemData> trashDataToCheck;
        [SerializeField] private float lastItemMaxVerticalMovementThreshold;
        [SerializeField] private float lastItemMinVerticalMovementThreshold;
        [Space]
        [SerializeField] private UnityEvent taskStarted;
        [SerializeField] private UnityEvent taskCompleted;
        [SerializeField] private UnityEvent trashPicked;
        [SerializeField] private UnityEvent allTrashPicked;
        [SerializeField] private UnityEvent hasNotAllTrashOnHand;

        private IHoldable _lastItemThrown;

        private int _amountOfTrashPicked;
        private bool _isCompleted;
        private bool _isStarted;
        
        //TODO: Create a class with all pickable objects in scene, get all items with ItemData of trashDataToCheck, create a list with all trash in scene, highlight all trash

        private void OnEnable()
        {
            eventToStartTask.EventRaised += OnEventToStartTaskRaised;
            eventToStopTask.EventRaised += OnEventToStopTaskRaised;
            eventToPreStartTask.EventRaised += OnEventToPreStartTaskRaised;

            handController.ItemAdded += OnItemAdded;
            handController.ItemRemoved += OnItemRemoved;

            TryAddAmountOfTrash();
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
            taskStarted?.Invoke();
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
            taskCompleted?.Invoke();
        }

        private void OnItemRemoved(IHoldable item)
        {
            if (item.WorldRepresentation.TryGetComponent<IObjectData>(out var objectData))
            {
                if (trashDataToCheck.Contains(objectData.Data))
                {
                    _amountOfTrashPicked--;
                    _lastItemThrown = item;

                    if (IsStarted) return;

                    HasAllTrashOnHand?.Invoke(PlayerHasTargetAmountOfTrashOnHand());
                }
            }
        }

        private void OnItemAdded(IHoldable item)
        {
            if (!PlayerHasTrashOnHand(item)) return;

            
            _amountOfTrashPicked++;
            trashPicked?.Invoke();

            if(PlayerHasTargetAmountOfTrashOnHand())
                allTrashPicked?.Invoke();
            else
            {
                hasNotAllTrashOnHand?.Invoke();
            }
            
            HasAllTrashOnHand?.Invoke(PlayerHasTargetAmountOfTrashOnHand());
        }

        private void TryAddAmountOfTrash()
        {
            foreach (var item in handController.ItemsOnHand)
            {
                if (!PlayerHasTrashOnHand(item)) continue;

                _amountOfTrashPicked++;
            }

            if(PlayerHasTargetAmountOfTrashOnHand())
                allTrashPicked?.Invoke();
            else
            {
                hasNotAllTrashOnHand?.Invoke();
            }
            HasAllTrashOnHand?.Invoke(PlayerHasTargetAmountOfTrashOnHand());
        }
        

        private bool PlayerHasTrashOnHand(IHoldable item)
        {
            if (!item.WorldRepresentation.TryGetComponent<IObjectData>(out var objectData)) return false;

            return trashDataToCheck.Contains(objectData.Data);
        }

        private bool PlayerHasTargetAmountOfTrashOnHand()
        {
            return _amountOfTrashPicked >= amountOfTrashToPick;
        }

        private IEnumerator CheckIfPlayerThrowAllTrash()
        {
            yield return new WaitUntil(() => _amountOfTrashPicked == 0);

            bool waitNextFrame = true;
            yield return new WaitUntil((() =>
            {
                if (waitNextFrame)
                {
                    waitNextFrame = false;
                    return false;
                }

                if (_lastItemThrown == null) return false;

                _lastItemThrown.WorldRepresentation.TryGetComponent<IThrowable>(out var throwable);
                var lastItemThrownVelocity = throwable.Rigidbody.velocity;

                bool hasVerticalMovement = lastItemThrownVelocity.y > lastItemMaxVerticalMovementThreshold ||
                                           lastItemThrownVelocity.y < lastItemMinVerticalMovementThreshold;
                return !hasVerticalMovement;
            }));
            CompleteTask();
        }
    }
}