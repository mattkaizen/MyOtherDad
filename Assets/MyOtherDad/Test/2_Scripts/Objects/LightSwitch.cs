﻿using Domain;
using UnityEngine;

namespace Objects
{
    public class LightSwitch : MonoBehaviour, ISwitchable
    {
        public bool IsActive => _isActive;
        private bool _isActive;
        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;

        }
    }
}