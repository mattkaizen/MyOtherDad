using Data;
using Interfaces;
using UnityEditor;

namespace EditorData
{
    [CustomEditor(typeof(ChangeableCameraEventChannelData))]
    public class ChangeableCameraEventChannelDataEditor : GenericEventChannelDataEditor<IChangeableCamera>
    {
    }
}