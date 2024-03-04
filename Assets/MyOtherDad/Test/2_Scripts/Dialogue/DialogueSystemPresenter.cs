using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class DialogueSystemPresenter : MonoBehaviour
    {
        [SerializeField] private DialogueSystem dialogueSystem;
        [SerializeField] private TMP_Text dialogueText;
        [Space]
        [Header("Animation settings")]
        [SerializeField] private float dialogueFadeInDuration;
        [SerializeField] private Ease dialogueFadeInEase;
        [Space]
        [SerializeField] private float dialogueFadeOutDuration;
        [SerializeField] private Ease dialogueFadeOutEase;

        private Tweener _fadeInDialogueText;
        private Tweener _fadeOutDialogueText;
        

        private void Awake()
        {
            dialogueSystem.DialogueEntryStarted += OnDialogueEntryStarted;
            dialogueSystem.DialogueEntryFinished += OnDialogueEntryFinished;
        }
        private void OnDialogueEntryStarted()
        {
            KillFadeTweens();
            FadeInDialogueText();
        }
        
        private void OnDialogueEntryFinished()
        {
            FadeOutDialogueText();
        }

        private void FadeInDialogueText()
        {
            _fadeInDialogueText = dialogueText.DOFade(1.0f, dialogueFadeInDuration).SetEase(dialogueFadeInEase);
        }
        
        private void FadeOutDialogueText()
        {
            _fadeOutDialogueText = dialogueText.DOFade(0.0f, dialogueFadeOutDuration).SetEase(dialogueFadeOutEase);
        }

        private void KillFadeTweens()
        {
            if(_fadeInDialogueText != null)
                _fadeInDialogueText.Kill();
            
            if(_fadeOutDialogueText != null)
                _fadeOutDialogueText.Kill();
        }
    }
}