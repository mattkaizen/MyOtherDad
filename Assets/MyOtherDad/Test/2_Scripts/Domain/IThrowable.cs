﻿using UnityEngine;

namespace Domain
{
    public interface IThrowable
    {
        void Throw(Vector3 direction, float force);
    }
}