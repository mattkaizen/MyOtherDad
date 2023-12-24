using System.Collections;
using Domain;
using Player;
using UnityEngine;

namespace Objects
{
    public class CollisionController : MonoBehaviour, IPlayerCollision
    {
        [SerializeField] private float overLapSphereRadius;
        [SerializeField] private LayerMask overLapSphereLayer;
        [SerializeField] private LayerMask defaultLayer;
        [SerializeField] private LayerMask ignorePlayerLayer;
        
        public void EnableCollisionWithPlayer()
        {
            StartCoroutine(EnableCollisionRoutine());
        }

        public void DisableCollisionWithPlayer()
        {
            SetLayer(ignorePlayerLayer);
        }

        private IEnumerator EnableCollisionRoutine()
        {
            yield return new WaitWhile(IsCollidingWithPlayer);
            SetLayer(defaultLayer);
        }

        private bool IsCollidingWithPlayer()
        {
            Collider[] interactingColliders = Physics.OverlapSphere(transform.position, overLapSphereRadius, overLapSphereLayer);

            foreach (var interactingCollider in interactingColliders)
            {
                if(interactingCollider.TryGetComponent<PlayerController>(out var player))
                {
                    return true;
                }
            }

            return false;
        }

        private void SetLayer(LayerMask layer)
        {
            int layerId = GetLayerIDFromMask(layer);
            
            if (layerId != -1)
            {
                gameObject.layer = layerId;
            }
        }

        private int GetLayerIDFromMask(LayerMask layerMask)
        {
            for (int i = 0; i < 32; i++)
            {
                if (layerMask == (layerMask | (1 << i)))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
