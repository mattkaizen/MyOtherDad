using System;
using System.Collections.Generic;
using UnityEngine;

public class GestureController : MonoBehaviour
{
    [SerializeField] private List<GestureSystem> gestures;

    private bool AreGesturesComplete()
    {
        return true;
    }
}