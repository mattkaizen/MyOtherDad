using System.Collections;
using System.Collections.Generic;
using CustomInput;
using Data;
using Domain;
using Objects;
using Player;
using UnityEngine;

namespace Tasks
{
    public class ThrowTrash : MonoBehaviour, ITask
    {
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }

        [Header("Event Channels to listen")] [SerializeField]
        private VoidEventChannelData eventToStartTask;

        [SerializeField] private VoidEventChannelData eventToStopTask;
        [SerializeField] private VoidEventChannelData eventToStartMiniGame;

        [Space] [Header("Broadcast on Event Channels")] [SerializeField]
        private VoidEventChannelData throwTrashTaskStarted;

        [SerializeField] private VoidEventChannelData throwTrashTaskStopped;
        [SerializeField] private VoidEventChannelData throwTrashTaskCompleted;

        [SerializeField] private TrashDetectorTrigger detectorTrigger;
        [SerializeField] private HandController handController;
        [SerializeField] private int amountOfTrashToPick;
        [SerializeField] private List<ItemData> trashDataToCheck;
        [SerializeField] private InputActionControlData getUpInput;

        private GameObject _lastItemThrown;

        private int _amountOfTrashPicked;
        private bool _isCompleted;
        private bool _isStarted;

        private void OnEnable()
        {
            eventToStartTask.EventRaised += OnEventToStartTaskRaised;
            eventToStopTask.EventRaised += OnEventToStopTaskRaised;
            eventToStartMiniGame.EventRaised += OnEventToStartMiniGameRaised;
            handController.ItemAdded += OnItemAdded;
            handController.ItemRemoved += OnItemRemoved;

            //TODO: EventToDisplayInterfaceRaised += TryDisplayInterface

            //TODO: Si la tarea inicio, al cambiar de camara a la CAMA, arranca el minijuego, mostrar x interfaz, bloquear GetUp input. 
        }

        private void OnEventToStartMiniGameRaised()
        {
            if (IsCompleted) return;

            if (IsStarted)
            {
                getUpInput.DisableInput();
                throwTrashTaskStarted.RaiseEvent();
                StartCoroutine(CheckIfPlayerThrowAllTrash());
            }
        }

        private void LateUpdate()
        {
            if (PlayerHasTargetAmountOfTrashOnHand())
            {
                IsStarted = true;
            }
            else
            {
                IsStarted = false;
            }
        }

        private void OnDisable()
        {
            eventToStartTask.EventRaised -= OnEventToStartTaskRaised;
            eventToStopTask.EventRaised -= OnEventToStopTaskRaised;
            handController.ItemAdded -= OnItemAdded;
            handController.ItemRemoved -= OnItemRemoved;
        }

        private void OnEventToStartTaskRaised()
        {
            StartTask();
        }

        private void OnEventToStopTaskRaised()
        {
            IsStarted = false;
        }

        public void StartTask()
        {
            IsStarted = true;
        }

        public void CompleteTask()
        {
            IsCompleted = true;
            getUpInput.EnableInput();
            throwTrashTaskCompleted.RaiseEvent();
        }

        private void OnItemRemoved(GameObject item)
        {
            if (item.TryGetComponent<IObjectData>(out var objectData))
            {
                if (trashDataToCheck.Contains(objectData.Data))
                {
                    _amountOfTrashPicked--;
                    _lastItemThrown = item;
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
                }
            }
        }

        private bool PlayerHasTargetAmountOfTrashOnHand() //TODO: Activar shader o representacion sobre la cama.
        {
            return _amountOfTrashPicked >= amountOfTrashToPick;
        }

        private IEnumerator CheckIfPlayerThrowAllTrash()
        {
            yield return new WaitUntil(() => _amountOfTrashPicked == 0);

            yield return new WaitUntil((() =>
            {
                _lastItemThrown.TryGetComponent<IThrowable>(out var throwable);
                return throwable.Rigidbody.velocity == Vector3.zero; //TODO: Tal vez con la velociddad vertical es suficiente
            }));
            CompleteTask();
        }
    }
}