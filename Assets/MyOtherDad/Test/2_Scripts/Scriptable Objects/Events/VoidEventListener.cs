using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelData eventToListen;
        [SerializeField] private UnityEvent eventRaised;

        private void OnEnable()
        {
            if (eventToListen != null)
                eventToListen.EventRaised += Response;
        }

        private void OnDisable()
        {
            if (eventToListen != null)
                eventToListen.EventRaised -= Response;
        }

        private void Response()
        {
            eventRaised?.Invoke();
        }
    }
}