using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDetector : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEngine.Camera camera;
    [SerializeField] private Image imageTest;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse over ui");
    }
}
