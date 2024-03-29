﻿using System.Collections.Generic;
using DG.Tweening;
using Domain;
using UnityEngine;
using UnityEngine.Events;

namespace Objects.UsableInteractable
{
    public class DirtyObject : MonoBehaviour, IItemInteractable
    {
        public bool IsCleaned { get; set; }
        
        public UnityEvent wasCleaned; 

        [SerializeField] private ItemData requiredItemToInteract;
        [SerializeField] private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
        [SerializeField] private float cleanFactor;
        [SerializeField] private float cleanTweenDuration;
        [SerializeField] private Ease cleanEase;
        [SerializeField] private bool startDirty;

        private readonly float _maxDirtAmount = 1.0f;
        private readonly float _minDirtAmount = 0.0f;

        private float _currentDirtAmount;

        private readonly int dirtyAmount = Shader.PropertyToID("_DirtAmount");

        private List<Material> _materials = new List<Material>();

        private void Awake()
        {
            foreach (var meshRenderer in meshRenderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    _materials.Add(material);
                }
            }

            if (startDirty && _materials != null)
            {
                SetDirtyAmount(_maxDirtAmount, 0);
            }
        }

        public bool TryInteractWith(ItemData item)
        {
            if (item == requiredItemToInteract)
            {
                if (!IsCleaned)
                    Clean();
                
                return true;
            }

            return false;
        }

        private void Clean()
        {
            float newDirtAmount = _currentDirtAmount - cleanFactor;
            newDirtAmount = Mathf.Clamp(newDirtAmount, _minDirtAmount, _maxDirtAmount);

            SetDirtyAmount(newDirtAmount, cleanTweenDuration);

            if (_currentDirtAmount <= _minDirtAmount)
            {
                IsCleaned = true;
                wasCleaned?.Invoke();
            }

        }

        private void SetDirtyAmount(float dirtyValue, float duration)
        {
            foreach (var material in _materials)
            {
                material.DOFloat(dirtyValue, dirtyAmount, duration)
                    .SetEase(cleanEase);
            }

            _currentDirtAmount = dirtyValue;
        }
    }
}