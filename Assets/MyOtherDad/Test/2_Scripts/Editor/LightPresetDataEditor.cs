using DayCycle;
using UnityEditor;

namespace EditorData
{
    [CustomEditor(typeof(LightningChanger))]
    public class LightPresetDataEditor : Editor
    {
        private Editor editorInstance;

        private void OnEnable()
        {
            editorInstance = null;
        }

        public override void OnInspectorGUI()
        {
            LightningChanger lightPresetData = (LightningChanger)target;
            if (editorInstance == null)
                editorInstance = Editor.CreateEditor(lightPresetData.Preset);

            base.OnInspectorGUI();
            editorInstance.DrawDefaultInspector();
        }
    }
}