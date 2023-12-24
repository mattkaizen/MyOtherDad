using UnityEngine;

namespace Objects
{
    public class HoldableItem : MonoBehaviour, IHoldable
    {
        public GameObject WorldRepresentation => gameObject;
        
        public GameObject HandRepresentation => handRepresentation;
        
        [SerializeField] private GameObject handRepresentation;

        public void TurnOnHandRepresentation()
        {
            handRepresentation.SetActive(true);
        }

        public void TurnOffHandRepresentation()
        {
            handRepresentation.SetActive(false);
        }
    }

    public interface IHoldable
    {
        GameObject WorldRepresentation { get; }
        GameObject HandRepresentation { get; }
        void TurnOnHandRepresentation();
        void TurnOffHandRepresentation();
    }
}