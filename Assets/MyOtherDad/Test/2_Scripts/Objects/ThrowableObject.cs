using Domain;
using UnityEngine;

public class ThrowableObject : MonoBehaviour, IThrowable
{
    public Rigidbody Rigidbody
    {
        get => objectRigidbody;
        set => objectRigidbody = value;
    }
    
    [SerializeField] private Rigidbody objectRigidbody;

    public void Throw(Vector3 direction, float force)
    {
        if (objectRigidbody != null)
            objectRigidbody.AddForce(direction * force, ForceMode.Impulse);
        
    }
}