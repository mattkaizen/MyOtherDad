using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObjectEffect : MonoBehaviour
{
    private Dictionary<Material, Color> materialsToSetColor = new Dictionary<Material, Color>();

    private Material _material;
    private static readonly int BaseColor = Shader.PropertyToID("Color");

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        ChangeColorOverTime();
    }

    private void ChangeColorOverTime()
    {
        Color color = _material.GetColor(BaseColor);

        float hue, sat, val;
        Color.RGBToHSV(color, out hue, out sat, out val);
        hue = (Time.time * 0.25f) % 1.0f;
        Debug.Log($"Hue {hue} Tiempo {Time.time}");

        color = Color.HSVToRGB(hue, sat, val);
        _material.SetColor(BaseColor, color);
    }
}
