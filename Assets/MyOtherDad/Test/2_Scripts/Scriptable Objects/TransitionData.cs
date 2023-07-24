using UnityEngine;


[CreateAssetMenu(menuName = "Transition Settings")]
public class TransitionData : ScriptableObject
{
    public float TransitionDuration => transitionDuration;
    
    [SerializeField] private float transitionDuration;
    
    
}