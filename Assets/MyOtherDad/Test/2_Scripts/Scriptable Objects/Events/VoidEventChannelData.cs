using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannelData : ScriptableObject
    {
        public UnityAction EventRaised = delegate { };

        public void RaiseEvent()
        {
            EventRaised?.Invoke();
        }
    }
}