using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerObjectThrower : MonoBehaviour
    {
        [SerializeField] private float defaultThrowForce = 10.0f;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private HandController handController;

        private void OnEnable()
        {
            inputReader.ThrowItem += TryThrowObject;
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
            if (!handController.HasItemOnHand) return;

            if (handController.CurrentItemOnHand.TryGetComponent<IThrowable>(out var throwable))
            { //TODO: tal vez haya que poner en kinematic el rigidbody antes de activar el game object y desactivar lo kinematic al lanzarlo.

                handController.CurrentItemOnHand.TryGetComponent<IPlayerCollision>(out var collision);

                // Vector3 throwDirection = handController.CurrentItemOnHand.transform.position + mainCamera.forward;
                // Vector3 throwDirection = shootPoint.localPosition + shootPoint.forward;
                Vector3 throwDirection = shootPoint.forward - shootPoint.localPosition;
                Debug.Log($"throw direction {throwDirection}");
                
                Debug.DrawRay(shootPoint.localPosition,  5 * mainCamera.forward, Color.magenta, 0.5f);

                // Debug.DrawLine(shootPoint.position, shootPoint.forward * 5, Color.magenta, 5);
                handController.TurnOffCurrentItemHandDisplay();
                handController.ResetParentCurrenItemHandRepresentation();
                handController.CurrentItemOnHand.transform.SetParent(null);
                handController.CurrentItemOnHand.transform.position = shootPoint.position;
                collision?.DisableCollisionWithPlayer();
                handController.CurrentItemOnHand.SetActive(true);
                collision?.EnableCollisionWithPlayer();
                throwable.Throw(throwDirection, defaultThrowForce);
                handController.RemoveCurrentItemOnHand();
                handController.TurnOnCurrentItemHandDisplay();

            }
            
        }
    }
}