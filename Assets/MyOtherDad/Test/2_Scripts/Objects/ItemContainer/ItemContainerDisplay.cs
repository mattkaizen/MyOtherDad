using Data;
using UnityEngine;

namespace Objects
{
    public class ItemContainerDisplay : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelData eventToDisplayGhostEffect;
        [SerializeField] private ItemContainer container;
        [SerializeField] private GameObject ghostModel;

        private void OnEnable()
        {
            if (container != null)
            {
                container.OnItemSet += DisplayItem;
                eventToDisplayGhostEffect.EventRaised += OnEventToDisplayGhostEffectRaised;
            }
        }

        private void OnDisable()
        {
            if (container != null)
            {
                container.OnItemSet += DisplayItem;
                eventToDisplayGhostEffect.EventRaised -= OnEventToDisplayGhostEffectRaised;
            }
        }
        private void OnEventToDisplayGhostEffectRaised()
        {
            TurnOnGhostItem();
        }

        private void TurnOnGhostItem()
        {
            ghostModel.SetActive(true);
        }

        private void DisplayItem()
        {
            ghostModel.SetActive(false);
            ChangeModelMaterial();
        }

        private void ChangeModelMaterial()
        {
            var newModel = Instantiate(container.ItemSet.Prefab, gameObject.transform);
            newModel.transform.localPosition = Vector3.zero;
            newModel.transform.localScale = Vector3.one;
            
            if(newModel.TryGetComponent<Collider>(out var collide))
            {
                collide.enabled = false;
            }
        }
    }
}