using DialogueEntry;
using UnityEngine;

namespace Dialogue
{
    public class DialogueEntry : MonoBehaviour
    {
        public DialogueEntryData Data => data;

        [SerializeField] private DialogueEntryData data;
    }
}