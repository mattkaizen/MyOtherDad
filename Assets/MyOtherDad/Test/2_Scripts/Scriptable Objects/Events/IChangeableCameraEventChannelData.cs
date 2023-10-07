using Interfaces;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Changeable Camera Event Channel", menuName = "Events/Changeable Camera Event Channel Data", order = 0)]
    public class ChangeableCameraEventChannelData : GenericEventChannelData<IChangeableCamera>
    {
        
    }
}