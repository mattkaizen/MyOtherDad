using Data;
using DG.Tweening;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PointerGesture
{
    public class PointerGestureMiniGamePresenter : MonoBehaviour
    {
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
        [SerializeField] private Image pencil;
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
        [SerializeField] private PointerGesturePointAnimator tutorialGestureAnimator;

        private Sequence _pencilSequence;

        private void OnEnable()
        {
            drawingTaskStarted.EventRaised += OnDrawingTaskStarted;
            drawingTaskTutorialStarted.EventRaised += OnDrawingTaskTutorialStarted;
            drawingTaskCompleted.EventRaised += OnDrawingTaskCompleted;
            tutorialCompleted.EventRaised += OnTutorialCompleted;
            tutorialRestarted.EventRaised += OnTutorialRestarted;
        }

        private void OnDisable()
        {
            drawingTaskStarted.EventRaised -= OnDrawingTaskStarted;
            drawingTaskTutorialStarted.EventRaised -= OnDrawingTaskTutorialStarted;
            drawingTaskCompleted.EventRaised -= OnDrawingTaskCompleted;
            tutorialCompleted.EventRaised -= OnTutorialCompleted;
            tutorialRestarted.EventRaised -= OnTutorialRestarted;

        }

        private void OnDrawingTaskStarted()
        {
            Debug.Log("PointerGestureMiniGamePresenter: Task started");
            DisplayMiniGameCursor();
            // DisplayTutorialUI();
        }

        private void OnDrawingTaskTutorialStarted()
        {
            Debug.Log("PointerGestureMiniGamePresenter: Task tutorial started");

            DisplayMiniGameCursor();
            DisplayTutorialUI();
        }

        private void OnDrawingTaskCompleted()
        {
            Debug.Log("PointerGestureMiniGamePresenter: Task completed");

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
            pencil.DOFade(0.0f, disableUIDuration).SetEase(disableUIEase);
            tutorialGestureAnimator.ScaleOutAnimation();
        }

        private void StartPencilAnimation()
        {
             _pencilSequence = DOTween.Sequence();

             _pencilSequence.Append(pencil.DOFade(0.0f, 0.0f));
             _pencilSequence.Append(pencil.rectTransform.DOAnchorPos(startPosition.anchoredPosition, 0.0f));
             _pencilSequence.Append(pencil.rectTransform.DOAnchorPos(endPosition.anchoredPosition, pencilMovementDuration)
                .SetEase(movementEase));
             _pencilSequence.Join(pencil.DOFade(1.0f, pencilFadeInDuration).SetEase(pencilFadeInEase));
             _pencilSequence.Insert(_pencilSequence.Duration(),
                pencil.DOFade(0.0f, pencilFadeOutDuration).SetEase(pencilFadeOutEase));

             _pencilSequence.SetLoops(-1, LoopType.Restart);
        }
    }
}