using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HighlightObjectEffect : MonoBehaviour
{
    [ColorUsage(true, true)] [SerializeField]
    private Color newEmissionColor;

    [ColorUsage(true, true)] [SerializeField]
    private Color blackEmissionColor;

    [SerializeField] private float lerpEmissiveColorToBaseColorTweenDuration;
    [SerializeField] private float lerpEmissiveColorToNewColorTweenDuration;

    private readonly int baseColor = Shader.PropertyToID("_BaseColor");
    private readonly int emissionColor = Shader.PropertyToID("_EmissionColor");

    private Dictionary<Material, Color> _materialsToSetColor = new Dictionary<Material, Color>();
    private List<Tweener> lerpEmissionColorToBaseColorTweeners = new List<Tweener>();
    private List<Tweener> lerpEmissionColorToNewColorTweeners = new List<Tweener>();
    private IEnumerator _highLightRoutine;

    private bool _isHighlighting;

    private void Awake()
    {
        Material[] materials = GetComponent<Renderer>().materials;

        foreach (var material in materials)
        {
            Color emissiveColor = material.GetColor(emissionColor);
            _materialsToSetColor.Add(material, emissiveColor);
        }
    }

    public void EnableHighLight()
    {
        if (!_isHighlighting)
        {
            KillLerpCurrenColorToBase();
            StartContinuousEmissiveColorChange();
            _isHighlighting = true;
            // _highLightRoutine = ContinuousColorChangeRoutine();
            // StartCoroutine(_highLightRoutine);
        }
    }

    public void DisableHighLight()
    {
        if (!_isHighlighting) return;
        //
        // if (_highLightRoutine != null)
        //     StopCoroutine(_highLightRoutine);

        KillEmissionColorToNewColorTweener();
        LerpCurrentEmissionColorToBase();
        _isHighlighting = false;
    }

    private void StartContinuousEmissiveColorChange()
    {
        lerpEmissionColorToNewColorTweeners.Clear();

        foreach (var materialToSetColor in _materialsToSetColor)
        {
            EnableEmission(materialToSetColor.Key);

            Tweener tweener = materialToSetColor.Key
                .DOColor(newEmissionColor, emissionColor, lerpEmissiveColorToNewColorTweenDuration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);

            lerpEmissionColorToNewColorTweeners.Add(tweener);
        }
    }

    // private IEnumerator ContinuousColorChangeRoutine()
    // {
    //     _isHighlighting = true;
    //
    //     while (_isHighlighting)
    //     {
    //         foreach (var materialToSetColor in _materialsToSetColor)
    //         {
    //             Color color = materialToSetColor.Key.GetColor(baseColor);
    //
    //             float hue, sat, val;
    //             Color.RGBToHSV(color, out hue, out sat, out val);
    //             hue = (Time.time * 0.25f) % 1.0f;
    //
    //             color = Color.HSVToRGB(hue, sat, val);
    //
    //
    //             materialToSetColor.Key.color = color;
    //         }
    //         yield return null;
    //     }
    // }


    private void LerpCurrentEmissionColorToBase()
    {
        foreach (var materialToSetColor in _materialsToSetColor)
        {
            Tweener tweener = materialToSetColor.Key.DOColor(blackEmissionColor, emissionColor,
                    lerpEmissiveColorToBaseColorTweenDuration)
                .OnComplete((() => { DisableEmission(materialToSetColor.Key); }));
            lerpEmissionColorToBaseColorTweeners.Add(tweener);
        }
    }

    private void KillEmissionColorToNewColorTweener()
    {
        foreach (var lerpEmissionColorToNewColorTweener in lerpEmissionColorToNewColorTweeners)
        {
            lerpEmissionColorToNewColorTweener.Kill();
        }

        lerpEmissionColorToNewColorTweeners.Clear();
    }

    private void KillLerpCurrenColorToBase()
    {
        foreach (var lerpToCurrentColorTweener in lerpEmissionColorToBaseColorTweeners)
        {
            lerpToCurrentColorTweener.Kill();
        }

        lerpEmissionColorToBaseColorTweeners.Clear();
    }

    private void EnableEmission(Material material)
    {
        material.EnableKeyword("_EMISSION");
        material.globalIlluminationFlags |= MaterialGlobalIlluminationFlags.RealtimeEmissive;
    }

    private void DisableEmission(Material material)
    {
        material.DisableKeyword("_EMISSION");
        material.globalIlluminationFlags &= ~MaterialGlobalIlluminationFlags.RealtimeEmissive;
    }
}