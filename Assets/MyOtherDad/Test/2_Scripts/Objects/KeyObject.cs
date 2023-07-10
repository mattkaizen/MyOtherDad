using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class KeyObject : ItemPickable
{
    public override void Pickup()
    {
        Debug.Log("Pickup");
        gameObject.SetActive(false);
    }
}
