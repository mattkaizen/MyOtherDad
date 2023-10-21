using Data;
using UnityEngine;

namespace Objects
{
    public class GameObjectStateSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject gameObjectToSwitchState;
        [SerializeField] private VoidEventChannelData eventToEnableGameObject;
        [SerializeField] private VoidEventChannelData eventToDisableGameObject;

        [SerializeField] private bool switcherCanBeReUsed;

        private void OnEnable()
        {
            eventToEnableGameObject.EventRaised += OnEventToEnableGameObjectRaised;
            eventToDisableGameObject.EventRaised += OnEventToDisableGameObjectRaised;
        }

        private void OnEventToDisableGameObjectRaised()
        {
            gameObjectToSwitchState.SetActive(false);

            if (switcherCanBeReUsed) return;
            
            gameObject.SetActive(false);

        }
        private void OnEventToEnableGameObjectRaised()
        {
            gameObjectToSwitchState.SetActive(true);
        }
    }
}