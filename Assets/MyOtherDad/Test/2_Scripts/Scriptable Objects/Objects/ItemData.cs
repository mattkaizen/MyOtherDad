using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string Name => currentName;
        
    [SerializeField] private string currentName;

}