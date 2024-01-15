    using UnityEngine;
    using Random = UnityEngine.Random;

    namespace Objects
{
    public class HoldableObject : MonoBehaviour, IHoldable
    {
        public GameObject WorldRepresentation => gameObject;
        
        public GameObject HandRepresentation => handRepresentation;
        
        [SerializeField] private GameObject handRepresentation;

        private void Awake()
        {
            handRepresentation.gameObject.name = $"Hand Representation: {WorldRepresentation.gameObject.name} {Random.Range(1,100)}";
        }

        public void TurnOnHandRepresentation()
        {
            handRepresentation.SetActive(true);
        }

        public void TurnOffHandRepresentation()
        {
            handRepresentation.SetActive(false);
        }
    }   
}