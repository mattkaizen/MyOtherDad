using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public abstract class GenericEventChannelData<T> : ScriptableObject
    {
        [Tooltip("The action to perform; Listeners subscribe to this UnityAction")]
        public UnityAction<T> EventRaised = delegate { };
 
        public void RaiseEvent(T parameter)
        {
            if (EventRaised == null)
                return;

            EventRaised.Invoke(parameter);
        }
    }
}