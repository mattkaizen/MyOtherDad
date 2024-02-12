using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class HighlightObjectEffect : MonoBehaviour
{
    [ColorUsage(true, true)] 
    [SerializeField] private Color newEmissionColor;

    [ColorUsage(true, true)]
    [SerializeField] private Color blackEmissionColor;

    [SerializeField] private float lerpEmissiveColorToBaseColorTweenDuration;
    [SerializeField] private float lerpEmissiveColorToNewColorTweenDuration;
    [SerializeField] private bool highLightChildren;

    private readonly int emissionColor = Shader.PropertyToID("_EmissionColor");

    private Dictionary<Material, Color> _materialsToSetColor = new Dictionary<Material, Color>();
    private List<Tweener> lerpEmissionColorToBaseColorTweeners = new List<Tweener>();
    private List<Tweener> lerpEmissionColorToNewColorTweeners = new List<Tweener>();
    private IEnumerator _highLightRoutine;

    private bool _isHighlighting;

    private void Awake()
    {
        if (highLightChildren)
        {
            if (TryGetComponent<Renderer>(out var parentRenderer))
            {
                foreach (var material in parentRenderer.materials)
                {
                    Color emissiveColor = material.GetColor(emissionColor);
                    _materialsToSetColor.Add(material, emissiveColor);
                }
            }

            Renderer[] childrenRenderer = GetComponentsInChildren<Renderer>();

            foreach (var childRenderer in childrenRenderer)
            {
                foreach (var childMaterial in childRenderer.materials)
                {
                    if (childMaterial.HasProperty(emissionColor))
                    {
                        Color emissiveColor = childMaterial.GetColor(emissionColor);
                        _materialsToSetColor.Add(childMaterial, emissiveColor);
                    }
                    else
                    {
                        Debug.Log($"Material: {childMaterial.name} doesn't have the property: {emissionColor.ToString()}");
                    }
                }
            }
        }
        else
        {
            
            if (TryGetComponent<Renderer>(out var parentRenderer))
            {
                foreach (var material in parentRenderer.materials)
                {
                    if (material.HasProperty(emissionColor))
                    {
                        Color emissiveColor = material.GetColor(emissionColor);
                        _materialsToSetColor.Add(material, emissiveColor);
                    }
                    else
                    {
                        Debug.Log($"Material: {material.name} doesn't have the property: {emissionColor.ToString()}");
                    }
                }
            }
        }
    }

    [UsedImplicitly]
    public void EnableHighLightFade()
    {
        if (!_isHighlighting)
        {
            KillLerpCurrenColorToBase();
            StartContinuousEmissiveColorChange();
            _isHighlighting = true;
        }
    }

    [UsedImplicitly]
    public void DisableHighLightFade()
    {
        if (!_isHighlighting) return;

        KillEmissionColorToNewColorTweener();
        LerpCurrentEmissionColorToBase(lerpEmissiveColorToBaseColorTweenDuration);
        _isHighlighting = false;
    }

    [UsedImplicitly]
    public void DisableHighLightImmediately()
    {
        KillEmissionColorToNewColorTweener();
        LerpCurrentEmissionColorToBase(0.0f);
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

    private void LerpCurrentEmissionColorToBase(float duration)
    {
        foreach (var materialToSetColor in _materialsToSetColor)
        {
            Tweener tweener = materialToSetColor.Key.DOColor(blackEmissionColor, emissionColor,
                    duration)
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