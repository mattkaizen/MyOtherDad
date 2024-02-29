using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class EventGroup : MonoBehaviour
    {
        [SerializeField] private UnityEvent response;

        [UsedImplicitly]
        public void Invoke()
        {
            response?.Invoke();
        }
    }
}