using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Minigame;
using UnityEngine;

public class GestureSystem : MonoBehaviour
{
    [SerializeField] private List<GesturePoint> gesturePoints;

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
    }

    private void GesturePoint_WasClicked()
    {
        StartFlashingCheckRoutine();
    }

    private void StartFlashingCheckRoutine()
    {
        _flashingCheckRoutine ??= FlashingCheckRoutine();

        if (_areCheckingGestures) return;
        Debug.Log("Check has Started");

        StartCoroutine(_flashingCheckRoutine);
    }

    private void StartCheckClickedGestures()
    {
        _checkGesturesRoutine ??= CheckGesturePointsInOrder();

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

    private IEnumerator FlashingCheckRoutine()
    {
        StartCheckClickedGestures();
        yield return new WaitForSeconds(3.0f);
        StopCheckClickedGestures();
    }

    private IEnumerator CheckClickedGesturesRoutine()
    {
        while (_areCheckingGestures)
        {
            if (AreGesturesPointClickedInOrder())
            {
                //Se completo en orden
                _isGestureComplete = true;
                _areCheckingGestures = false;
                break;
            }

            yield return null;
        }
    }

    private void TryResetGestures()
    {
        if (AreAllGesturesPointClicked()) return;

        foreach (var gesture in gesturePoints)
        {
            gesture.ResetPoint();
        }
    }

    private bool AreAllGesturesPointClicked()
    {
        return gesturePoints.All(x => x.WasClicked);
    }

    private bool AreGesturesPointClickedInOrder()
    {
        for (int i = 0; i < gesturePoints.Count; i++)
        {
            if (!gesturePoints[i].WasClicked)
            {
                return false;
            }
        }

        return true;
    }

    private IEnumerator CheckGesturePointsInOrder()
    {
        int index = 0;

        while (_areCheckingGestures)
        {
            if (index >= gesturePoints.Count)
                break;

            if (gesturePoints[index].WasClicked)
            {
                Debug.Log($"Gesture point correct {index}");

                if (AreAllGesturesPointClicked())
                {
                    Debug.Log("Gesture points Completed in order");
                    //Se Completo el gesto
                    break;
                }
                index++;
            }
            else
            {
                Debug.Log("Gesture point Incorrect");

                //Se clickeo el incorrecto
                //Resetear
                break;
            }

            yield return new WaitUntil(HaveAnyRemainingPointBeenCompleted);
            Debug.Log("Cicle");
        }
    }

    private bool HaveAnyRemainingPointBeenCompleted()
    {
        var remainingGesturePoints = gesturePoints.Where(x => !x.WasClicked).ToList();

        return remainingGesturePoints.Any(x => x.WasClicked); //es un clon
    }
}