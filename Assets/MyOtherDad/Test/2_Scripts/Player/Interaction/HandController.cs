using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class HandController : MonoBehaviour
    {
        public event UnityAction<GameObject> ItemAdded = delegate {  };
        public event UnityAction<GameObject> ItemRemoved = delegate {  };
        public GameObject CurrentItemOnHand
        {
            get => _currentItemOnHand;
            set => _currentItemOnHand = value;
        }
        
        public List<GameObject> ItemsOnHand
        {
            get => _itemsOnHand;
            set => _itemsOnHand = value;
        }

        public bool HasItemOnHand => CurrentItemOnHand;
        public bool HasMaxItemOnTheHand => maxAmountItemsOnTheHand == AmountOfItemsOnTheHand;
        public int AmountOfItemsOnTheHand => _itemsOnHand.Count;

        [SerializeField] private int maxAmountItemsOnTheHand = 5;
        [SerializeField] private GameObject hand;
        [SerializeField] private InputReaderData inputReader;

        private List<GameObject> _itemsOnHand = new List<GameObject>();
        private GameObject _currentItemOnHand;

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
            item.HandRepresentation.transform.localPosition = Vector3.zero;

            
            TurnOffCurrentItemHandDisplay();
            // _itemsOnHand.Add(item.WorldRepresentation);
            _currentItemOnHandIndex = 0;

            _itemsOnHand.Insert(_currentItemOnHandIndex,item.WorldRepresentation );
            
            item.TurnOnHandRepresentation();
            
            CurrentItemOnHand = _itemsOnHand[_currentItemOnHandIndex];
            
            ItemAdded?.Invoke(item.WorldRepresentation);
            
            Debug.Log($"Add item on hand index {_currentItemOnHandIndex} { item.WorldRepresentation.gameObject.name}");
        }

        public void TurnOnCurrentItemHandDisplay()
        {
            if (CurrentItemOnHand == null) return;
            
            if (CurrentItemOnHand.TryGetComponent<IHoldable>(out var newHoldable))
            {
                newHoldable.TurnOnHandRepresentation();
            }
        }
        
        public void TurnOffCurrentItemHandDisplay()
        {
            if (CurrentItemOnHand == null) return;

            if (CurrentItemOnHand.TryGetComponent<IHoldable>(out var holdable))
            {
                
                Debug.Log($"Turn off {holdable.HandRepresentation.gameObject.name} in {holdable.WorldRepresentation.gameObject.name}");
                holdable.TurnOffHandRepresentation();
            }
            else
            {
                Debug.Log($"it doesn't have holdable component {_currentItemOnHand.name}");

            }
        }

        public void ResetParentCurrenItemHandRepresentation()
        {
            if (CurrentItemOnHand == null) return;

            if (CurrentItemOnHand.TryGetComponent<IHoldable>(out var holdable))
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
    }
}