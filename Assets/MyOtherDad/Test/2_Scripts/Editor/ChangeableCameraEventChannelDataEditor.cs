using Data;
using Domain;
using UnityEditor;

namespace EditorData
{
    [CustomEditor(typeof(ChangeableCameraEventChannelData))]
    public class ChangeableCameraEventChannelDataEditor : GenericEventChannelDataEditor<IInteractableCamera>
    {
    }
}