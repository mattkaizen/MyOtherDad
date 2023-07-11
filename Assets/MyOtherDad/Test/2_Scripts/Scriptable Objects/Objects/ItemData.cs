using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string Name => currentName;
    public GameObject Prefab => currentPrefab;
        
    [SerializeField] private string currentName;
    [SerializeField] private GameObject currentPrefab;

}