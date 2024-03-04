using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lean.Localization;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        public event UnityAction DialogueEntryStarted;
        public event UnityAction DialogueEntryQueue;
        public event UnityAction DialogueEntryFinished;

        [SerializeField] private LeanLocalizedBehaviour dialogueUI;

        private Queue<DialogueEntry> _priorityEntries = new Queue<DialogueEntry>();
        private DialogueEntry _currentDialogueEntry;
        private IEnumerator _showDialogueEntryRoutine;

        private bool _isShowingDialogueEntry;

        [UsedImplicitly]
        public void TryShowDialogue(DialogueEntry dialogueEntry)
        {
            if (!_isShowingDialogueEntry)
            {
                _currentDialogueEntry = dialogueEntry;
                StartShowDialogueEntryRoutine(true);
                return;
            }

            if (!dialogueEntry.Data.HasPriority) return;

            if (_currentDialogueEntry.Data.HasPriority)
            {
                Debug.Log($"Added dialogue entry {dialogueEntry.Data.TranslationName}");
                if (_currentDialogueEntry.Data.ShowUntilInterrupted)
                {
                    _currentDialogueEntry = dialogueEntry;
                    StartShowDialogueEntryRoutine(false);   
                }
                else
                {
                    _priorityEntries.Enqueue(dialogueEntry);
                }
            }
            else
            {
                StopShowDialogueEntryRoutine();

                _currentDialogueEntry = dialogueEntry;
                StartShowDialogueEntryRoutine(false);
            }
        }

        private void SetDialogueEntry(DialogueEntry dialogue)
        {
            dialogueUI.TranslationName = dialogue.Data.TranslationName;
        }

        private void StartShowDialogueEntryRoutine(bool isFirstDialogueEntry)
        {
            _showDialogueEntryRoutine = ShowDialogueEntryRoutine(_currentDialogueEntry, isFirstDialogueEntry);

            StartCoroutine(_showDialogueEntryRoutine);
        }

        private void StopShowDialogueEntryRoutine()
        {
            if (_showDialogueEntryRoutine != null)
                StopCoroutine(_showDialogueEntryRoutine);
        }

        private IEnumerator ShowDialogueEntryRoutine(DialogueEntry dialogueEntry, bool isFirstDialogueEntry)
        {
            Debug.Log($"isFirstDialogueEntry{isFirstDialogueEntry}");

            if (isFirstDialogueEntry)
            {
                DialogueEntryStarted?.Invoke();
            }

            _isShowingDialogueEntry = true;
            SetDialogueEntry(dialogueEntry);

            if (dialogueEntry.Data.ShowUntilInterrupted) yield break;
            
            yield return new WaitForSeconds(dialogueEntry.Data.TimeToShowDialogue);

            if (_priorityEntries.Count > 0)
            {
                Debug.Log($"Start new dialogue entry {dialogueEntry.Data.TranslationName}");

                DialogueEntry nextDialogueEntry = _priorityEntries.Dequeue();
                _currentDialogueEntry = nextDialogueEntry;
                StartShowDialogueEntryRoutine(false);
            }
            else
            {
                _isShowingDialogueEntry = false;
                DialogueEntryFinished?.Invoke();
            }
        }
    }
}