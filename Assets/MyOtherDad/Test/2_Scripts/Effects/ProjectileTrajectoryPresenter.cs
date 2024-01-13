using CustomInput;
using Data;
using Domain;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Effects
{
    public class ProjectileTrajectoryPresenter : MonoBehaviour
    {
        [Header("Dependencies")] [SerializeField]
        private VoidEventChannelData eventToStartDisplayTrajectory;

        [SerializeField] private VoidEventChannelData eventToStopDisplayTrajectory;
        [SerializeField] private PlayerObjectThrower playerObjectThrower;
        [SerializeField] private HandController handController;
        [SerializeField] private InputActionControlData lookAtAsset;

        [Header("Line Settings")] [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField] private int linePoints;
        [SerializeField] private float timeBetweenPoints = 0.1f;
        [SerializeField] private LayerMask collisionLayer;

        private bool _isDrawingTrajectory;

        private void OnEnable()
        {
            lookAtAsset.Input.performed += OnLookAtPerformed;
            eventToStartDisplayTrajectory.EventRaised += OnEventToStartDrawTrajectoryRaised;
            eventToStopDisplayTrajectory.EventRaised += OnEventToStopDrawTrajectoryRaised;
        }

        private void OnDisable()
        {
            lookAtAsset.Input.performed -= OnLookAtPerformed;
            eventToStartDisplayTrajectory.EventRaised -= OnEventToStartDrawTrajectoryRaised;
            eventToStopDisplayTrajectory.EventRaised -= OnEventToStopDrawTrajectoryRaised;
        }

        private void OnEventToStartDrawTrajectoryRaised()
        {
            _isDrawingTrajectory = true;
            DrawProjection();
        }

        private void OnEventToStopDrawTrajectoryRaised()
        {
            lineRenderer.enabled = false;
            _isDrawingTrajectory = false;
        }

        private void OnLookAtPerformed(InputAction.CallbackContext obj)
        {
            // if (!_isDrawingTrajectory) return;
            //
            // DrawProjection();
        }

        private void FixedUpdate() //TODO: Si la velocidad del mouse supera un threshold, no dibujar la proyeccion
        {
            if (!_isDrawingTrajectory) return;
            
            DrawProjection();
        }

        private void DrawProjection()
        {
            if (!handController.HasItemOnHand ||
                !handController.CurrentItemOnHand.WorldRepresentation.TryGetComponent<IThrowable>(out var throwable))
            {
                lineRenderer.enabled = false;
                return;
            }

            lineRenderer.enabled = true;
            lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;

            Vector3 startPosition = playerObjectThrower.ShootPoint.position;
            Vector3 startVelocity = playerObjectThrower.CurrentThrowForce * playerObjectThrower.ShootPoint.forward;

            int i = 0;
            lineRenderer.SetPosition(i, startPosition);

            for (float time = 0.0f; time < linePoints; time += timeBetweenPoints)
            {
                Vector3 point = startPosition + time * startVelocity;
                point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2.0f * time * time);

                Vector3 lastPosition = lineRenderer.GetPosition(i);

                if (Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit,
                        (point - lastPosition).magnitude, collisionLayer))
                {
                    lineRenderer.SetPosition(i + 1, hit.point);
                    lineRenderer.positionCount = i + 2;
                    return;
                }

                i++;
                lineRenderer.SetPosition(i, point);
            }
        }
    }
}