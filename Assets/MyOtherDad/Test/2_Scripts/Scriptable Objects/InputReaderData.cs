using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//TODO: Modificar PlayerObjectPicker para que utilice esta clase para suscribirse 

[CreateAssetMenu(fileName = "InputReaderData")]
public class InputReaderData : ScriptableObject, GameControls.IPlayerActions
{
    private GameControls _playerInputActions;
    private InputAction _interact;
    public event UnityAction Interacted = delegate { };

    private void OnEnable()
    {
        _playerInputActions = new GameControls();
        _playerInputActions.Player.Enable();

        _interact = _playerInputActions.Player.Interact;
        _interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        _interact.performed -= OnInteract;
        _playerInputActions.Player.Disable();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (_interact.WasPerformedThisFrame())
        {
            Interacted?.Invoke();
        }
    }
}