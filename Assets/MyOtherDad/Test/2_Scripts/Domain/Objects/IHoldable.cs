using UnityEngine;

namespace Objects
{
    public interface IHoldable
    {
        GameObject WorldRepresentation { get; }
        GameObject HandRepresentation { get; }
        void TurnOnHandRepresentation();
        void TurnOffHandRepresentation();
    }
}