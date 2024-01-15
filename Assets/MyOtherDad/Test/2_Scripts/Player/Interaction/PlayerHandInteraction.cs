using Domain.Objects;
using UnityEngine;

namespace Player
{
    public class PlayerHandInteraction : MonoBehaviour
    {
        [SerializeField] private HandController handController;
        [SerializeField] private InputReaderData inputReader;

        private void OnEnable()
        {    
            inputReader.Interacting += OnInteracting;
        }

        private void OnDisable()
        {
            inputReader.Interacting -= OnInteracting;
        }

        private void OnInteracting(bool isInteracting)
        {
            if (isInteracting)
            {
                if (!handController.HasItemOnHand) return;

                if (handController.CurrentItemOnHand.HandRepresentation.TryGetComponent<IUsable>(out var usable))
                {
                    if (handController.CurrentItemOnHand.HandRepresentation.TryGetComponent<IContinuousUsable>(out var continuousUsable))
                    {
                        continuousUsable.IsBeingUsed = true;
                    }
                    usable.Use();
                }   
            }
            else
            {
                if (!handController.HasItemOnHand) return;
                
                if (handController.CurrentItemOnHand.HandRepresentation.TryGetComponent<IContinuousUsable>(out var continuousUsable))
                {
                    continuousUsable.IsBeingUsed = false;
                    continuousUsable.StopUsing();
                }   
            }

        }
    }
}