using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class TrashDetectorTrigger : MonoBehaviour
    {
        [SerializeField] private List<ItemData> trashData;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<ThrowableObject>(out var throwableObject))
            {
                //ToDo: Si el objeto no se encuentra en la lista y su velocidad es 0, entonces lo agrega en la lista.
                
                //TODo: Tal vez crear HandManager, que permita switchear con Q y E los objetos de la mano
            }
        }
    }
}