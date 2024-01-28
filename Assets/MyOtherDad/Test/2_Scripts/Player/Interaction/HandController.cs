using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class HandController : MonoBehaviour
    {
        public event UnityAction<IHoldable> ItemAdded = delegate { };
        public event UnityAction<IHoldable> ItemRemoved = delegate { };

        public IHoldable CurrentItemOnHand
        {
            get => _currentItemOnHand;
            set => _currentItemOnHand = value;
        }

        public List<IHoldable> ItemsOnHand
        {
            get => _itemsOnHand;
            set => _itemsOnHand = value;
        }

        public bool HasItemOnHand
        {
            get
            {
                if (CurrentItemOnHand != null)
                    return true;

                return false;
            }
        }

        public bool HasMaxItemOnTheHand => maxAmountItemsOnTheHand == AmountOfItemsOnTheHand;
        public int AmountOfItemsOnTheHand => _itemsOnHand.Count;

        [SerializeField] private int maxAmountItemsOnTheHand = 5;
        [SerializeField] private GameObject hand;
        [SerializeField] private InputReaderData inputReader;

        private List<IHoldable> _itemsOnHand = new List<IHoldable>();
        private IHoldable _currentItemOnHand;

        private int _currentItemOnHandIndex;

        private void OnEnable()
        {
            inputReader.SwitchedNextItem += TrySwitchNextItem;
            inputReader.SwitchedPreviousItem += TrySwitchPreviousItem;
        }

        private void OnDisable()
        {
            inputReader.SwitchedNextItem -= TrySwitchNextItem;
            inputReader.SwitchedPreviousItem -= TrySwitchPreviousItem;
        }

        private void TrySwitchNextItem()
        {
            if (!HasItemOnHand) return;
            if (AmountOfItemsOnTheHand <= 1) return;

            int nextItemIndex = _currentItemOnHandIndex + 1;

            if (nextItemIndex >= _itemsOnHand.Count) return;

            TurnOffCurrentItemHandDisplay();

            _currentItemOnHandIndex += 1;
            CurrentItemOnHand = _itemsOnHand[_currentItemOnHandIndex];

            TurnOnCurrentItemHandDisplay();

            Debug.Log($"Switch Next Item, current index {_currentItemOnHandIndex}");
        }

        private void TrySwitchPreviousItem()
        {
            if (!HasItemOnHand) return;
            if (AmountOfItemsOnTheHand <= 1) return;

            int nextItemIndex = _currentItemOnHandIndex - 1;

            if (nextItemIndex < 0) return;

            _currentItemOnHandIndex -= 1;

            TurnOffCurrentItemHandDisplay();

            CurrentItemOnHand = _itemsOnHand[_currentItemOnHandIndex];

            TurnOnCurrentItemHandDisplay();
        }

        public void AddItemOnHand(IHoldable item)
        {
            item.WorldRepresentation.transform.SetParent(hand.transform);
            item.WorldRepresentation.transform.localPosition = Vector3.zero;
            item.WorldRepresentation.transform.localRotation = Quaternion.identity;

            item.HandRepresentation.transform.SetParent(hand.transform);

            TurnOffCurrentItemHandDisplay();
            _currentItemOnHandIndex = 0;

            _itemsOnHand.Insert(_currentItemOnHandIndex, item);

            item.TurnOnHandRepresentation();

            CurrentItemOnHand = _itemsOnHand[_currentItemOnHandIndex];

            ItemAdded?.Invoke(item);
        }

        public void TurnOnCurrentItemHandDisplay()
        {
            if (CurrentItemOnHand == null) return;

            CurrentItemOnHand.TurnOnHandRepresentation();
        }

        public void TurnOffCurrentItemHandDisplay()
        {
            if (CurrentItemOnHand == null) return;

            CurrentItemOnHand.TurnOffHandRepresentation();
        }

        public void ResetParentCurrentItemHandRepresentation()
        {
            if (CurrentItemOnHand == null) return;

            if (CurrentItemOnHand.WorldRepresentation.TryGetComponent<IHoldable>(out var holdable))
            {
                holdable.HandRepresentation.transform.SetParent(holdable.WorldRepresentation.transform);
                holdable.HandRepresentation.transform.localPosition = Vector3.zero;
            }
        }

        public void RemoveCurrentItemOnHand()
        {
            if (AmountOfItemsOnTheHand == 0)
            {
                return;
            }

            ItemRemoved?.Invoke(_currentItemOnHand);
            _itemsOnHand.RemoveAt(_currentItemOnHandIndex);

            if (AmountOfItemsOnTheHand == 0)
            {
                CurrentItemOnHand = null;
            }
            else if (AmountOfItemsOnTheHand == 1)
            {
                CurrentItemOnHand = _itemsOnHand[0];
                _currentItemOnHandIndex = 0;
            }
            else
            {
                int lastIndex = _itemsOnHand.Count - 1;
                bool isMinorThanLastIndex = _currentItemOnHandIndex < lastIndex;

                if (_currentItemOnHandIndex > 0 && isMinorThanLastIndex)
                {
                    _currentItemOnHandIndex += 1;
                }
                else if (_currentItemOnHandIndex == _itemsOnHand.Count)
                {
                    _currentItemOnHandIndex -= 1;
                }

                CurrentItemOnHand = _itemsOnHand[_currentItemOnHandIndex];
            }
        }

        public void ClearCurrentItemOnHandDisplay()
        {
            TurnOffCurrentItemHandDisplay();
            ResetParentCurrentItemHandRepresentation();
            CurrentItemOnHand.WorldRepresentation.transform.SetParent(null);
            RemoveCurrentItemOnHand();
            TurnOnCurrentItemHandDisplay();
        }
    }
}