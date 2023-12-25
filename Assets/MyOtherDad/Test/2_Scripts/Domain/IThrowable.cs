using UnityEngine;

namespace Domain
{
    public interface IThrowable
    {
        Rigidbody Rigidbody { get; }
        void Throw(Vector3 direction, float force);
    }
}