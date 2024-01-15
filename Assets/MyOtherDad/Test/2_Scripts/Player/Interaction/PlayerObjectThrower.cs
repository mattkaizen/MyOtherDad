using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerObjectThrower : MonoBehaviour
    {
        public Vector3 ThrowDirection => _throwDirection;

        public float CurrentThrowForce => _currentThrowForce;

        public Transform ShootPoint => shootPoint;

        [SerializeField] private float defaultThrowForce = 10.0f;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private HandController handController;

        private Vector3 _throwDirection;

        private float _currentThrowForce;

        private void OnEnable()
        {
            inputReader.ThrowItem += TryThrowObject;
            SetDefaultThrowForce();
        }

        private void OnDisable()
        {
            inputReader.ThrowItem -= TryThrowObject;
        }
        
        private void OnDrawGizmos()
        {
            Debug.DrawRay(shootPoint.position,  5 * mainCamera.forward, Color.yellow, 0.5f);
        }
        private void TryThrowObject()
        {
            if (!handController.HasItemOnHand)
            {
                Debug.Log("Hasn't item on hand");
                return;
            }

            if (handController.CurrentItemOnHand.WorldRepresentation.TryGetComponent<IThrowable>(out var throwable))
            {
                handController.CurrentItemOnHand.WorldRepresentation.TryGetComponent<IPlayerCollision>(out var collision);
                
                Debug.DrawRay(shootPoint.localPosition,  5 * mainCamera.forward, Color.magenta, 0.5f);
                
                _throwDirection = shootPoint.forward;

                handController.TurnOffCurrentItemHandDisplay();
                handController.ResetParentCurrentItemHandRepresentation();
                handController.CurrentItemOnHand.WorldRepresentation.transform.SetParent(null);
                handController.CurrentItemOnHand.WorldRepresentation.transform.position = shootPoint.position;
                collision?.DisableCollisionWithPlayer();
                handController.CurrentItemOnHand.WorldRepresentation.SetActive(true);
                collision?.EnableCollisionWithPlayer();
                throwable.Throw(_throwDirection, _currentThrowForce);
                handController.RemoveCurrentItemOnHand();
                handController.TurnOnCurrentItemHandDisplay();
                
            }
            
        }

        public void SetDefaultThrowForce()
        {
            _currentThrowForce = defaultThrowForce;
        }

        public void SetNewThrowForce(float newForce)
        {
            _currentThrowForce = newForce;
        }
    }
}