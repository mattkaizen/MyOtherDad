using CustomInput;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderData", menuName = "Input/Reader")]
public class InputReaderData : ScriptableObject, GameControls.IPlayerActions
{
    public event UnityAction<Vector2> Moved = delegate { };
    public event UnityAction<Vector2> Looked = delegate { };
    public event UnityAction<bool> Ran = delegate { };
    public event UnityAction<bool> Interacting = delegate { };
    public event UnityAction GettingUp = delegate { };
    public event UnityAction Painting = delegate { };
    public event UnityAction Painted = delegate { };
    public event UnityAction ThrowItem = delegate { };
    public event UnityAction SwitchedNextItem = delegate { };
    public event UnityAction SwitchedPreviousItem = delegate { };

    [SerializeField] private InputActionControlManagerData inputActionControlManager;
    [SerializeField] private InputActionReference lookAsset;

    private GameControls _playerInputActions;

    private void OnEnable()
    {
        _playerInputActions = new GameControls();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.SetCallbacks(this);

        inputActionControlManager.MoveActionControl.Input = _playerInputActions.Player.Move;
        inputActionControlManager.RunActionControl.Input = _playerInputActions.Player.Run;
        inputActionControlManager.LookAtActionControl.Input = _playerInputActions.Player.LookAt;
        inputActionControlManager.LookAtAssetActionControl.Input = lookAsset.action;
        inputActionControlManager.InteractActionControl.Input = _playerInputActions.Player.Interact;
        inputActionControlManager.GetUpActionControl.Input = _playerInputActions.Player.GetUp;
        inputActionControlManager.ThrowItemActionControl.Input = _playerInputActions.Player.ThrowItem;
        inputActionControlManager.SwitchNextItemActionControl.Input = _playerInputActions.Player.SwitchNextItem;
        inputActionControlManager.SwitchPreviousItemActionControl.Input = _playerInputActions.Player.SwitchPreviousItem;
        inputActionControlManager.PaintActionControl.Input = _playerInputActions.Player.Paint;

        inputActionControlManager.MoveActionControl.Input.performed += OnMove;
        inputActionControlManager.RunActionControl.Input.performed += OnRun;
        inputActionControlManager.LookAtActionControl.Input.performed += OnLookAt;
        inputActionControlManager.InteractActionControl.Input.performed += OnInteract;
        inputActionControlManager.GetUpActionControl.Input.performed += OnGetUp;
        inputActionControlManager.ThrowItemActionControl.Input.performed += OnThrowItem;
        inputActionControlManager.SwitchNextItemActionControl.Input.performed += OnSwitchNextItem;
        inputActionControlManager.SwitchPreviousItemActionControl.Input.performed += OnSwitchPreviousItem;
        inputActionControlManager.PaintActionControl.Input.performed += OnPaint;
    }

    private void OnDisable()
    {
        inputActionControlManager.MoveActionControl.Input.performed -= OnMove;
        inputActionControlManager.RunActionControl.Input.performed -= OnRun;
        inputActionControlManager.LookAtActionControl.Input.performed -= OnLookAt;
        inputActionControlManager.InteractActionControl.Input.performed -= OnInteract;
        inputActionControlManager.GetUpActionControl.Input.performed -= OnGetUp;
        inputActionControlManager.ThrowItemActionControl.Input.performed -= OnThrowItem;
        inputActionControlManager.SwitchNextItemActionControl.Input.performed -= OnSwitchNextItem;
        inputActionControlManager.SwitchPreviousItemActionControl.Input.performed -= OnSwitchPreviousItem;
        inputActionControlManager.PaintActionControl.Input.performed -= OnPaint;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.InteractActionControl.Input.WasPerformedThisFrame())
        {
            Interacting?.Invoke(true);
        }

        if (inputActionControlManager.InteractActionControl.Input.WasReleasedThisFrame())
        {
            Interacting?.Invoke(false);
        }
    }

    public void OnGetUp(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.GetUpActionControl.Input.WasPerformedThisFrame())
        {
            GettingUp?.Invoke();
        }
    }

    public void OnPaint(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.PaintActionControl.Input.WasPerformedThisFrame())
        {
            Painting?.Invoke();
        }

        if (inputActionControlManager.PaintActionControl.Input.WasReleasedThisFrame())
        {
            Painted?.Invoke();
        }
    }

    public void OnSwitchNextItem(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.SwitchNextItemActionControl.Input.WasPerformedThisFrame())
        {
            SwitchedNextItem?.Invoke();
        }
    }

    public void OnSwitchPreviousItem(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.SwitchPreviousItemActionControl.Input.WasPerformedThisFrame())
        {
            SwitchedPreviousItem?.Invoke();
        }
    }

    public void OnThrowItem(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.ThrowItemActionControl.Input.WasPerformedThisFrame())
        {
            ThrowItem?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.MoveActionControl.Input.WasPerformedThisFrame())
        {
            Moved?.Invoke(context.ReadValue<Vector2>());
        }
        else
        {
            Moved?.Invoke(Vector2.zero);
        }
    }

    public void OnLookAt(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.LookAtActionControl.Input.WasPerformedThisFrame())
        {
            Looked?.Invoke(context.ReadValue<Vector2>());
        }
        else
        {
            Looked?.Invoke(Vector2.zero);
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (inputActionControlManager.RunActionControl.Input.WasPerformedThisFrame())
        {
            Ran?.Invoke(true);
        }

        if (inputActionControlManager.RunActionControl.Input.WasReleasedThisFrame())
        {
            Ran?.Invoke(false);
        }
    }
}