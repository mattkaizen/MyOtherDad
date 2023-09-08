using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Minigame;
using UnityEngine;

public class GestureSystem : MonoBehaviour
{
    [SerializeField] private List<GesturePoint> gesturePoints;

    private IEnumerator _checkGesturesRoutine;

    private bool _areCheckingGestures;
    private bool _isGestureComplete;

    private void Awake()
    {
        foreach (var gesturePoint in gesturePoints)
        {
            gesturePoint.wasClicked += GesturePoint_WasClicked;
        }
    }

    private void OnDisable()
    {
        foreach (var gesturePoint in gesturePoints)
        {
            gesturePoint.wasClicked -= GesturePoint_WasClicked;
        }
    }

    private void GesturePoint_WasClicked()
    {
        StartCheckClickedGestures();
    }

    private void StartCheckClickedGestures()
    {
        _checkGesturesRoutine ??= CheckClickedGesturesRoutine();

        if (_areCheckingGestures) return;
        
        _areCheckingGestures = true;
        StartCoroutine(_checkGesturesRoutine);
    }

    private void StopCheckClickedGestures()
    {
        if (_checkGesturesRoutine == null) return;

        StopCoroutine(_checkGesturesRoutine);
        _areCheckingGestures = false;
    }

    private IEnumerator FlashingCheckRoutine()
    {
        StartCheckClickedGestures();
        yield return new WaitForSeconds(10.0f);
        StopCheckClickedGestures();
    }

    private IEnumerator CheckClickedGesturesRoutine()
    {
        while (_areCheckingGestures)
        {
            if (AreAllGesturesPointClicked())
            {
                break;
            }

            yield return null;
        }

        _isGestureComplete = true;
        _areCheckingGestures = false;
    }

    private void TryResetGestures()
    {
        if (AreAllGesturesPointClicked()) return;

        foreach (var gesture in gesturePoints)
        {
            gesture.ResetGesture();
        }
    }

    private bool AreAllGesturesPointClicked()
    {
        return gesturePoints.All(x => x.WasClicked);
    }
}