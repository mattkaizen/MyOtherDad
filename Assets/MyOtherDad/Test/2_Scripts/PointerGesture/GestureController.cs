using System;
using System.Collections.Generic;
using PointerGesture;
using UnityEngine;

public class GestureController : MonoBehaviour
{
    [SerializeField] private List<PointerGestureChecker> gestures;

    private bool AreGesturesComplete()
    {
        return true;
    }
}