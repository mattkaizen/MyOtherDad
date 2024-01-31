using Data;
using Player;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureMiniGamePresenter : MonoBehaviour
    {
        [Header("Listen to Event Channels")]
        [SerializeField] private VoidEventChannelData drawingTaskStarted;
        [SerializeField] private VoidEventChannelData drawingTaskTutorialStarted;
        [SerializeField] private VoidEventChannelData drawingTaskCompleted;
        [SerializeField] private CursorChanger pencilCursor;

        private void OnEnable()
        {
            drawingTaskStarted.EventRaised += OnDrawingTaskStarted;
            drawingTaskTutorialStarted.EventRaised += OnDrawingTaskTutorialStarted;
            drawingTaskCompleted.EventRaised += OnDrawingTaskCompleted;
        }

        private void OnDisable()
        {
            drawingTaskStarted.EventRaised -= OnDrawingTaskStarted;
            drawingTaskTutorialStarted.EventRaised -= OnDrawingTaskTutorialStarted;
            drawingTaskCompleted.EventRaised -= OnDrawingTaskCompleted;
        }

        private void OnDrawingTaskStarted()
        {
            Debug.Log("PointerGestureMiniGamePresenter: Task started");
            pencilCursor.ChangeCursorImage();
            pencilCursor.EnableCursorImage();
        }
        
        private void OnDrawingTaskTutorialStarted()
        {
            Debug.Log("PointerGestureMiniGamePresenter: Task tutorial started");
            
            pencilCursor.ChangeCursorImage();
            pencilCursor.EnableCursorImage();
        }
        
        private void OnDrawingTaskCompleted()
        {
            Debug.Log("PointerGestureMiniGamePresenter: Task completed");

            pencilCursor.DisableCursorImage();
        }
    }
}