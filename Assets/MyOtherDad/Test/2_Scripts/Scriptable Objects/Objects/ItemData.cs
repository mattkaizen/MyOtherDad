using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string Name => currentName;

    public int ID => id;

    public GameObject Prefab => prefab;

    [SerializeField] private int id;
    [SerializeField] private string currentName;
    [SerializeField] private GameObject prefab;

}