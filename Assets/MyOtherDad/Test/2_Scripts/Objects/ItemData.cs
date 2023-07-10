using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Key")]
public class ItemData : ScriptableObject
{
    public Color Color => color;
    [SerializeField] private Color color;
}