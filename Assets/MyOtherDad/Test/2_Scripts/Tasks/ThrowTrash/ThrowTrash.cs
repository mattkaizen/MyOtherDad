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

        public bool IsPreStarted
        {
            get => _isPreStarted;
            set => _isPreStarted = value;
        }

        public VoidEventChannelData ThrowTrashTaskStopped => throwTrashTaskStopped;

        public IntEventChannelData ThrowTrashTaskCompletedWithScoreOf => throwTrashTaskCompletedWithScoreOf;

        [Header("Event Channels to listen")] [SerializeField]
        private VoidEventChannelData eventToPreStartTask;

        [SerializeField] private VoidEventChannelData eventToStartTask;

        [SerializeField] private VoidEventChannelData eventToStopTask;

        [Space] [Header("Broadcast on Event Channels")] [SerializeField]
        private VoidEventChannelData throwTrashTaskPreStarted;

        [SerializeField] private VoidEventChannelData throwTrashTaskStarted;

        [SerializeField] private VoidEventChannelData throwTrashTaskStopped;
        [SerializeField] private IntEventChannelData throwTrashTaskCompletedWithScoreOf;
        [SerializeField] private VoidEventChannelData enableCameraObjectInputInterrupted;

        [Header("Dependencies")] [SerializeField]
        private TrashDetectorTrigger trashDetectorTrigger;

        [SerializeField] private HandController handController;
        [SerializeField] private PlayerObjectThrower playerObjectThrower;
        [SerializeField] private InputActionControlManagerData inputActionManager;

        [Header("Task settings")] [SerializeField]
        private float newThrowForce = 20.0f;

        [SerializeField] private int amountOfTrashToPick;
        [SerializeField] private List<ItemData> trashDataToCheck;
        [SerializeField] private float lastItemMaxVerticalMovementThreshold;
        [SerializeField] private float lastItemMinVerticalMovementThreshold;
        [Space]
        [SerializeField] private UnityEvent taskCompleted;

        private IHoldable _lastItemThrown;

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
            if (item.WorldRepresentation.TryGetComponent<IObjectData>(out var objectData))
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