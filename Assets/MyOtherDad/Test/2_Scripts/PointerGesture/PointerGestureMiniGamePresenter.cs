using Data;
using DG.Tweening;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PointerGesture
{
    public class PointerGestureMiniGamePresenter : MonoBehaviour
    {
        [SerializeField] private UnityEvent tutorialGestureCompleted;
        [Space]
        [Header("Listen to Event Channels")]
        [SerializeField] private VoidEventChannelData drawingTaskStarted;
        [SerializeField] private VoidEventChannelData drawingTaskTutorialStarted;
        [SerializeField] private VoidEventChannelData drawingTaskCompleted;
        [SerializeField] private VoidEventChannelData tutorialCompleted;
        [SerializeField] private VoidEventChannelData tutorialRestarted;
        [SerializeField] private CursorChanger pencilCursor;

        [Space]
        [Header("Pencil Tween Settings")]
        [Header("Movement")]
        [SerializeField] private Image tutorialPencil;
        [SerializeField] private float pencilMovementDuration;
        [SerializeField] private Ease movementEase;
        [SerializeField] private RectTransform startPosition;
        [SerializeField] private RectTransform endPosition;
        [Header("Fade In")]
        [SerializeField] private float pencilFadeInDuration;
        [SerializeField] private Ease pencilFadeInEase;
        [Header("Fade Out")]
        [SerializeField] private float pencilFadeOutDuration;
        [SerializeField] private Ease pencilFadeOutEase;

        [Space]
        [Header("Tutorial text Settings")]
        [SerializeField] private TMP_Text tutorialText;
        [Header("Fade In")]
        [SerializeField] private float tutorialTextFadeInDuration;
        [SerializeField] private Ease tutorialTextFadeInEase;
        [Space]
        [Header("Disable UI settings")]
        [SerializeField] private float disableUIDuration;
        [SerializeField] private Ease disableUIEase;
        [SerializeField] private PointerGesture tutorialGestureAnimator;

        private Sequence _pencilSequence;

        private void OnEnable()
        {
            drawingTaskStarted.EventRaised += OnDrawingTaskStarted;
            drawingTaskTutorialStarted.EventRaised += OnDrawingTaskTutorialStarted;
            drawingTaskCompleted.EventRaised += OnDrawingTaskCompleted;
            tutorialCompleted.EventRaised += OnTutorialCompleted;
            tutorialRestarted.EventRaised += OnTutorialRestarted;
            tutorialGestureAnimator.Checker.GestureCompleted += OnTutorialGestureComplete;
        }
        
        private void OnDisable()
        {
            drawingTaskStarted.EventRaised -= OnDrawingTaskStarted;
            drawingTaskTutorialStarted.EventRaised -= OnDrawingTaskTutorialStarted;
            drawingTaskCompleted.EventRaised -= OnDrawingTaskCompleted;
            tutorialCompleted.EventRaised -= OnTutorialCompleted;
            tutorialRestarted.EventRaised -= OnTutorialRestarted;
            tutorialGestureAnimator.Checker.GestureCompleted -= OnTutorialGestureComplete;
        }

        private void OnDrawingTaskStarted()
        {
            DisplayMiniGameCursor();
        }

        private void OnDrawingTaskTutorialStarted()
        {
            DisplayMiniGameCursor();
            DisplayTutorialUI();
        }

        private void OnDrawingTaskCompleted()
        {
            pencilCursor.DisableCursorImage();
        }
        
        private void OnTutorialRestarted()
        {
            tutorialGestureAnimator.gameObject.SetActive(false);
            DisplayTutorialUI();
        }

        private void OnTutorialCompleted()
        {
            HideTutorialUI();
        }
        
        private void OnTutorialGestureComplete()
        {
            _pencilSequence.Kill();
            FadeOutTutorialPencil();
            tutorialGestureCompleted?.Invoke();
        }

        private void DisplayTutorialUI()
        {
            StartPencilAnimation();
            tutorialText.DOFade(1.0f, tutorialTextFadeInDuration).SetEase(tutorialTextFadeInEase);
            tutorialGestureAnimator.gameObject.SetActive(true);
        }

        private void DisplayMiniGameCursor()
        {
            pencilCursor.ChangeCursorImage();
            pencilCursor.EnableCursorImage();
        }

        private void HideTutorialUI()
        {
            _pencilSequence.Kill();
            tutorialText.DOFade(0.0f, disableUIDuration).SetEase(disableUIEase);
            FadeOutTutorialPencil();
        }

        private void FadeOutTutorialPencil()
        {
            tutorialPencil.DOFade(0.0f, disableUIDuration).SetEase(disableUIEase);
            tutorialGestureAnimator.Animator.ScaleOutAnimation();
        }

        private void StartPencilAnimation()
        {
             _pencilSequence = DOTween.Sequence();

             _pencilSequence.Append(tutorialPencil.DOFade(0.0f, 0.0f));
             _pencilSequence.Append(tutorialPencil.rectTransform.DOAnchorPos(startPosition.anchoredPosition, 0.0f));
             _pencilSequence.Append(tutorialPencil.rectTransform.DOAnchorPos(endPosition.anchoredPosition, pencilMovementDuration)
                .SetEase(movementEase));
             _pencilSequence.Join(tutorialPencil.DOFade(1.0f, pencilFadeInDuration).SetEase(pencilFadeInEase));
             _pencilSequence.Insert(_pencilSequence.Duration(),
                tutorialPencil.DOFade(0.0f, pencilFadeOutDuration).SetEase(pencilFadeOutEase));

             _pencilSequence.SetLoops(-1, LoopType.Restart);
        }
    }
}