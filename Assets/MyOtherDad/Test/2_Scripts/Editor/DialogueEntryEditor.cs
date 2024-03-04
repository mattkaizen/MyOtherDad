#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    [CustomEditor(typeof(DialogueEntry))]
    public class DialogueEntryEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DialogueEntry dialogueEntry = (DialogueEntry)target;

            if (dialogueEntry.Data != null)
            {
                EditorGUI.BeginChangeCheck();

                dialogueEntry.Data.HasPriority = EditorGUILayout.Toggle("Has Priority", dialogueEntry.Data.HasPriority);
                dialogueEntry.Data.ShowUntilInterrupted = EditorGUILayout.Toggle("Show Until Interrupted", dialogueEntry.Data.ShowUntilInterrupted);
                dialogueEntry.Data.TimeToShowDialogue = EditorGUILayout.FloatField("Time to Show Dialogue",
                    dialogueEntry.Data.TimeToShowDialogue);

                // Dibujar Translation Name y el botón List en la misma línea
                EditorGUILayout.BeginHorizontal();
                dialogueEntry.Data.TranslationName = EditorGUILayout.TextField("Translation Name",dialogueEntry.Data.TranslationName);
                if (GUILayout.Button("List", GUILayout.Width(50)))
                {
                    ShowTranslationList(dialogueEntry);
                }

                EditorGUILayout.EndHorizontal();

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility
                        .SetDirty(dialogueEntry); // Marcar el objeto como modificado para que los cambios se guarden
                }
            }
        }

        private void ShowTranslationList(DialogueEntry dialogueEntry)
        {
            var menu = new GenericMenu();

            if (!string.IsNullOrEmpty(dialogueEntry.Data.TranslationName))
            {
                // Agregar una opción para seleccionar la traducción actual
                menu.AddItem(new GUIContent("Current: " + dialogueEntry.Data.TranslationName), false, null);
                menu.AddItem(GUIContent.none, false, null); // Separador
            }

            // Agregar opciones para seleccionar traducciones existentes en LeanLocalization
            foreach (var translationName in Lean.Localization.LeanLocalization.CurrentTranslations.Keys)
            {
                menu.AddItem(new GUIContent(translationName), dialogueEntry.Data.TranslationName == translationName,
                    () =>
                    {
                        dialogueEntry.Data.TranslationName = translationName;
                        EditorUtility
                            .SetDirty(
                                dialogueEntry); // Marcar el objeto como modificado para que los cambios se guarden
                    });
            }

            if (menu.GetItemCount() > 0)
            {
                menu.ShowAsContext();
            }
            else
            {
                Debug.LogWarning("No translations found in Lean Localization.");
            }
        }
    }
}
#endif