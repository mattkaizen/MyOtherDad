using Data;
using Objects;
using UnityEngine;

namespace Tasks
{
    public class ArrangeBooks : MonoBehaviour, ITask 
    {
        //TODO: Crear interfaz que muestre cantidad de libros colocados y los libros por colocar, crear shader parecido a colocar los objetos en phasmofobia

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

        public int AmountOfBooksToSet
        {
            get
            {
                if (bookContainers != null)
                    return bookContainers.Length;

                return 0;
            }
        }
        
        private int _amountOfBookSet;

        [Header("Event Channels to listen")]
        [SerializeField] private VoidEventChannelData eventToStartArrangeBooksTask;
        [SerializeField] private VoidEventChannelData eventToStopArrangeBooks;
        [Space] 
        [Header("Broadcast on Event Channels")]
        [SerializeField] private VoidEventChannelData arrangeBooksTaskStarted;
        [SerializeField] private VoidEventChannelData arrangeBooksTaskStopped;
        [SerializeField] private VoidEventChannelData arrangeBooksTaskCompleted;
        [SerializeField] private IntEventChannelData amountOfBookSetChanged;
        [SerializeField] private ItemContainer[] bookContainers;

        private bool _isCompleted;
        private bool _isStarted;

        private void OnEnable()
        {
            eventToStartArrangeBooksTask.EventRaised += OnEventToStartTaskRaised;
            // eventToStopArrangeBooks.EventRaised += OnEventToStopTaskRaised;

            foreach (var bookContainer in bookContainers)
            {
                bookContainer.OnItemSet += UpdateAmountBookSet;
            }
        }

        private void OnDisable()
        {
            eventToStartArrangeBooksTask.EventRaised -= OnEventToStartTaskRaised;
            // eventToStopArrangeBooks.EventRaised -= OnEventToStopTaskRaised;
        }

        public void StartTask()
        {
            foreach (var bookContainer in bookContainers)
            {
                bookContainer.gameObject.SetActive(true);
            }
            IsStarted = true;
        }

        public void CompleteTask()
        {
            IsCompleted = true;
            arrangeBooksTaskCompleted.RaiseEvent();
        }
        
        private void UpdateAmountBookSet()
        {
            _amountOfBookSet++;
            amountOfBookSetChanged.RaiseEvent(_amountOfBookSet);

            if (AreAllBooksSet())
            {
                CompleteTask();
            }
        }

        private void OnEventToStopTaskRaised()
        {
            TryStopTask();
        }

        private void OnEventToStartTaskRaised()
        {
            if (IsStarted) return;

            StartTask();
            arrangeBooksTaskStarted.RaiseEvent();
        }
        
        private void TryStopTask()
        {
            if (IsCompleted) return;

            IsStarted = false;
            arrangeBooksTaskStopped.RaiseEvent();
        }

        private bool AreAllBooksSet()
        {
            return _amountOfBookSet == AmountOfBooksToSet;
        }
    }
}