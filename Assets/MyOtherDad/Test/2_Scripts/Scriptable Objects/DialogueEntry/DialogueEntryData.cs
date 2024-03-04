using Lean.Localization;
using UnityEngine;

namespace DialogueEntry
{
    [CreateAssetMenu(fileName = "DialogueEntry_", menuName = "Dialogue/Entry", order = 0)]
    public class DialogueEntryData : ScriptableObject
    {
        public string TranslationName
        {
            get => translationName;
            set => translationName = value;
        }
        public bool HasPriority
        {
            get => hasPriority;
            set => hasPriority = value;
        }
        public float TimeToShowDialogue
        {
            get => timeToShowDialogue;
            set => timeToShowDialogue = value;
        }
        public bool ShowUntilInterrupted
        {
            get => showUntilInterrupted;
            set => showUntilInterrupted = value;
        }

        [SerializeField] private bool hasPriority;
        [SerializeField] private bool showUntilInterrupted;
        [SerializeField] private float timeToShowDialogue;
        [LeanTranslationName]
        [SerializeField] private string translationName;
    }
}