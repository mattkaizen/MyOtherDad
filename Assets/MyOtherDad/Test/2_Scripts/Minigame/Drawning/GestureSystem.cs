using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Minigame;
using UnityEngine;

public class GestureSystem : MonoBehaviour
{
    [SerializeField] private List<GesturePoint> gesturePoints;
    [SerializeField] private VoidEventChannelData onGestureCompleted;

    private List<GesturePoint> _incompleteGesturePoints;

    private IEnumerator _checkGesturesRoutine;
    private IEnumerator _flashingCheckRoutine;

    private bool _areCheckingGestures;
    private bool _isGestureComplete;

    private void Awake()
    {
        Initialize();
    }

    private void OnDisable()
    {
        foreach (var gesturePoint in gesturePoints)
        {
            gesturePoint.wasClicked -= GesturePoint_WasClicked;
        }
    }

    private void Initialize()
    {
        foreach (var gesturePoint in gesturePoints)
        {
            gesturePoint.wasClicked += GesturePoint_WasClicked;
        }

        _incompleteGesturePoints = CloneGesturePointList();
    }

    private void GesturePoint_WasClicked()
    {
        if (_areCheckingGestures) return;

        StartFlashingCheck();
    }

    private IEnumerator FlashingCheckRoutine()
    {
        Debug.Log($"Flashing Check has Started {_areCheckingGestures}");
        StartCheckClickedGestures();
        yield return new WaitForSeconds(3.0f);
        ResetCheckingSystem();
    }

    private IEnumerator CheckGesturePointsInOrder()
    {
        Debug.Log($"Check has Started {_areCheckingGestures}");

        int index = 0;

        while (_areCheckingGestures)
        {
            if (index >= gesturePoints.Count)
                break;

            if (gesturePoints[index].WasClicked)
            {
                Debug.Log($"Gesture point correct {index}");
                _incompleteGesturePoints.Remove(gesturePoints[index]);

                if (AreAllGesturesPointClicked())
                {
                    Debug.Log("Gesture points Completed in order");
                    //Se Completo el gesto
                    StopFlashingCheckRoutine();
                    _isGestureComplete = true;
                    onGestureCompleted.RaiseEvent();
                    break;
                }

                index++;
            }
            else
            {
                Debug.Log("Gesture point Incorrect");
                ResetCheckingSystem();
                break;
            }

            yield return new WaitUntil(HaveAnyRemainingPointBeenCompleted);
            Debug.Log("Cicle");
        }
    }

    private void StartFlashingCheck()
    {
        _flashingCheckRoutine = FlashingCheckRoutine();
        StartCoroutine(_flashingCheckRoutine);
    }

    private void StartCheckClickedGestures()
    {
        _checkGesturesRoutine = CheckGesturePointsInOrder();
        _areCheckingGestures = true;

        StartCoroutine(_checkGesturesRoutine);
    }

    private void StopCheckClickedGestures()
    {
        if (_checkGesturesRoutine == null) return;

        StopCoroutine(_checkGesturesRoutine);
        Debug.Log("Stop Checking");
        _areCheckingGestures = false;
    }

    private void StopFlashingCheckRoutine()
    {
        if (_flashingCheckRoutine == null) return;

        StopCoroutine(_flashingCheckRoutine);
        Debug.Log("Stop flashing Checking");
    }

    private void ResetCheckingSystem()
    {
        ResetGesturesPoint();
        _incompleteGesturePoints = CloneGesturePointList();
        StopCheckClickedGestures();
        StopFlashingCheckRoutine();

        Debug.Log($"System rested {_areCheckingGestures}");
    }

    private void ResetGesturesPoint()
    {
        foreach (var gesture in gesturePoints)
        {
            gesture.ResetPoint();
        }
    }

    private List<GesturePoint> CloneGesturePointList()
    {
        return new List<GesturePoint>(gesturePoints);
    }

    private bool HaveAnyRemainingPointBeenCompleted()
    {
        return _incompleteGesturePoints.Any(x => x.WasClicked);
    }

    private bool AreAllGesturesPointClicked()
    {
        return gesturePoints.All(x => x.WasClicked);
    }
}