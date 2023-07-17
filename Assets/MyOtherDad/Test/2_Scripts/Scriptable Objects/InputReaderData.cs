using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//TODO: Modificar PlayerObjectPicker para que utilice esta clase para suscribirse 

[CreateAssetMenu(fileName = "InputReaderData")]
public class InputReaderData : ScriptableObject, GameControls.IPlayerActions
{
    public event UnityAction Interacted = delegate { };
    public event UnityAction GotUp = delegate { };
    
    private GameControls _playerInputActions;
    private InputAction _interact;
    private InputAction _getUp;

    private void OnEnable()
    {
        _playerInputActions = new GameControls();
        _playerInputActions.Player.Enable();

        _interact = _playerInputActions.Player.Interact;
        _getUp = _playerInputActions.Player.GetUp;
        
        _interact.performed += OnInteract;
        _getUp.performed += OnGetUp;
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

    public void OnGetUp(InputAction.CallbackContext context)
    {
        if (_getUp.WasPerformedThisFrame())
        {
            GotUp?.Invoke();
        }
    }
}