using UnityEngine;
using UnityEngine.Events;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannelData : ScriptableObject
    {
        public event UnityAction OnEventRaised = delegate { };

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}