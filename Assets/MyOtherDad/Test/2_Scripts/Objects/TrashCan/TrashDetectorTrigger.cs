using System.Collections;
using System.Collections.Generic;
using Domain;
using UnityEngine;

namespace Objects
{
    public class TrashDetectorTrigger : MonoBehaviour
    {
        public int AmountObjectAdded
        {
            get => amountObjectAdded;
            set => amountObjectAdded = value;
        }

        public bool IsTrashDetectionEnabled
        {
            get => _isTrashDetectionEnabled;
            set => _isTrashDetectionEnabled = value;
        }

        [SerializeField] private List<ItemData> trashDataToCheck;
        [SerializeField] private int amountObjectAdded;

        private Dictionary<GameObject, IEnumerator> _currentObjects = new Dictionary<GameObject, IEnumerator>();

        private bool _isTrashDetectionEnabled;

        private void OnTriggerStay(Collider other)
        {
            if (!_isTrashDetectionEnabled) return;
            
            if (other.TryGetComponent<IThrowable>(out var throwableObject))
            {
                if (other.TryGetComponent<IObjectData>(out var objectData))
                {
                    if (trashDataToCheck.Contains(objectData.Data))
                    {
                        if (!_currentObjects.ContainsKey(other.gameObject))
                        {
                            IEnumerator checkIfHasMovementRoutine = CheckIfRigidbodyHasMovementRoutine(other.gameObject);
                            StartCoroutine(checkIfHasMovementRoutine);
                            
                            _currentObjects.Add(other.gameObject, checkIfHasMovementRoutine);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isTrashDetectionEnabled) return;

            if (_currentObjects.ContainsKey(other.gameObject))
            {
                _currentObjects.Remove(other.gameObject);
            }
        }

        private IEnumerator CheckIfRigidbodyHasMovementRoutine(GameObject objectToCheck)
        {
            if (objectToCheck.TryGetComponent<IThrowable>(out var throwable))
            {
                yield return new WaitWhile((() =>
                {
                    var hasMovement = throwable.Rigidbody.velocity != Vector3.zero;
                    return hasMovement;
                }));
                AddObject();
            }
        }

        private void AddObject()
        {
            amountObjectAdded++;
        }
    }
}