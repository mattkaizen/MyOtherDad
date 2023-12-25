using System.Collections;
using System.Collections.Generic;
using Domain;
using UnityEngine;

namespace Objects
{
    public class TrashDetectorTrigger : MonoBehaviour
    {
        [SerializeField] private List<ItemData> trashDataToCheck;
        [SerializeField] private Vector3 velocityThreshold;
        [SerializeField] private int amountObjectAdded;

        private Dictionary<GameObject, IEnumerator> _currentObjects = new Dictionary<GameObject, IEnumerator>();

        private void OnTriggerStay(Collider other)
        {
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
                //ToDo: Si el objeto no se encuentra en la lista y su velocidad es 0, entonces lo agrega en la lista.
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
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