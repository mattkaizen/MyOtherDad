using Data;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{
    [Header("Player Input actions")] [SerializeField]
    private InputActionReference lookAsset;
    [SerializeField] private InputActionReference moveAsset;

    [Space] [Header("Listen to Event Channels")] [SerializeField]
    private VoidEventChannelData normalTransitionStarted;
    [SerializeField] private VoidEventChannelData normalTransitionEnded;
    [SerializeField] private VoidEventChannelData resetTransitionStarted;
    [SerializeField] private VoidEventChannelData resetTransitionEnded;


    private void Awake()
    {
        normalTransitionStarted.EventRaised += OnNormalTransitionStarted;
        normalTransitionEnded.EventRaised += OnNormalTransitionEnded;
        resetTransitionStarted.EventRaised += OnResetTransitionStarted;
        resetTransitionEnded.EventRaised += OnResetTransitionEnded;
    }

    private void OnDisable()
    {
        
        normalTransitionStarted.EventRaised -= OnNormalTransitionStarted;
        normalTransitionEnded.EventRaised -= OnNormalTransitionEnded;
        resetTransitionStarted.EventRaised -= OnResetTransitionStarted;
        resetTransitionEnded.EventRaised -= OnResetTransitionEnded;
    }

    private void OnResetTransitionStarted()
    {
        DisableInput(lookAsset.action);
    }

    private void OnResetTransitionEnded()
    {
        EnableInput(lookAsset.action);
        EnableInput(moveAsset.action);
    }

    private void OnNormalTransitionStarted()
    {
        DisableInput(lookAsset.action);
        DisableInput(moveAsset.action);
    }

    private void OnNormalTransitionEnded()
    {
        EnableInput(lookAsset.action);
    }

    private void EnableInput(InputAction input)
    {
        input?.Enable();
    }

    private void DisableInput(InputAction input)
    {
        input?.Disable();
    }
}