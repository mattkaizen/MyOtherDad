using System.Collections;
using Domain;
using Domain.Objects;
using UnityEngine;

namespace Objects
{
    public class ContinuousUsableObject : MonoBehaviour, IContinuousUsable
    {
        public bool IsBeingUsed { get; set; }

        [SerializeField] private ObjectDataProvider dataProvider;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float usingTime;

        private IUsableInteractable _currentUsableInteractable;
        private IEnumerator _usingObjectRoutine;

        public void Use()
        {
            StartUsingObjectCoroutine();
        }

        public void StopUsing()
        {
            StopUsingObjectCoroutine();
        }
        private IEnumerator UsingObjectRoutine()
        {
            while (IsBeingUsed)
            {
                TryInteractWithUsable(); //TODO: Tal vez agregar un evento para cuando consigue interactuar con el objeto
                yield return new WaitForSeconds(usingTime);
            }
        }
        
        private void TryInteractWithUsable()
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log($"Cloth Hit {hitInfo.transform.gameObject.name}"); //TODO: Dibujar el rayo, choca con la pared
                if (hitInfo.transform.gameObject.TryGetComponent<IItemInteractable>(out var interactable))
                {
                    interactable.TryInteractWith(dataProvider.Data);
                }
            }
        }
        
        private void StartUsingObjectCoroutine()
        {
            _usingObjectRoutine = UsingObjectRoutine();

            StartCoroutine(_usingObjectRoutine);
        }

        private void StopUsingObjectCoroutine()
        {
            if (_usingObjectRoutine != null)
                StopCoroutine(_usingObjectRoutine);
        }
    }
}