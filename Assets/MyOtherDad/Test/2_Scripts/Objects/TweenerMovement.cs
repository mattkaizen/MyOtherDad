using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects
{
    public class TweenerMovement : MonoBehaviour
    {
        [SerializeField] private GameObject objectToTween;
        [SerializeField] private Vector3 targetRotation;
        [SerializeField] private float movementSpeed;
        [SerializeField] private Ease movementEase;

        private Tweener _currentTween;
        private Vector3 _initialRotation;

        private readonly float _movementScaleFactor = 10;
        
        private void Awake()
        {
            _initialRotation = objectToTween.transform.localEulerAngles;
        }

        [UsedImplicitly]
        public void KillCurrentTween()
        {
            _currentTween?.Kill();
        }
        
        [UsedImplicitly]
        public void TweenLocalRotationToTargetRotation()
        {
            KillCurrentTween();
            _currentTween = objectToTween.transform.DOLocalRotate(targetRotation, GetMovementSpeed()).SetEase(movementEase)
                .SetSpeedBased(true);
        }
        
        [UsedImplicitly]
        public void TweenLocalRotationToInitialRotation()
        {
            KillCurrentTween();
            _currentTween = objectToTween.transform.DOLocalRotate(_initialRotation, GetMovementSpeed()).SetEase(movementEase)
                .SetSpeedBased(true);
        }

        private float GetMovementSpeed()
        {
            return movementSpeed * _movementScaleFactor;
        }
    }
}