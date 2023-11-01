using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderData")]
public class InputReaderData : ScriptableObject, GameControls.IPlayerActions
{
    public event UnityAction Interacted = delegate { };
    public event UnityAction GettingUp = delegate { };
    public event UnityAction<Vector2> Moved = delegate { };
    public event UnityAction<Vector2> Looked = delegate { };
    public event UnityAction Painting = delegate { };
    public event UnityAction Painted = delegate { };
    public event UnityAction<bool> Ran = delegate { };
    
    public InputAction Move => _move;
    public InputAction Look => _look;
    public InputAction LookAsset => lookAsset.action;
    public InputAction Run => _run;
    public InputAction Interact => _interact;
    public InputAction Paint => _paint;
    public InputAction GetUp => _getUp;
    
    [SerializeField] private InputActionReference lookAsset;
    
    private GameControls _playerInputActions;
    private InputAction _interact;
    private InputAction _getUp;
    private InputAction _move;
    private InputAction _look;
    private InputAction _run;
    private InputAction _paint;

    public List<InputAction> playerInputActions = new List<InputAction>();

    private void OnEnable()
    {
        _playerInputActions = new GameControls();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.SetCallbacks(this);

        _interact = _playerInputActions.Player.Interact;
        _getUp = _playerInputActions.Player.GetUp;
        _move = _playerInputActions.Player.Move;
        _look = _playerInputActions.Player.LookAt;
        _run = _playerInputActions.Player.Run;
        _paint = _playerInputActions.Player.Paint;

        _interact.performed += OnInteract;
        _getUp.performed += OnGetUp;
        _move.performed += OnMove;
        _run.performed += OnRun;
        _paint.performed += OnPainting;
        
        playerInputActions.Clear();
        
        playerInputActions.Add(_interact);
        playerInputActions.Add(_getUp);
        playerInputActions.Add(_move);
        playerInputActions.Add(_look);
        playerInputActions.Add(_run);
        playerInputActions.Add(_paint);
        playerInputActions.Add(lookAsset.action);

    }

    private void OnDisable()
    {
        _interact.performed -= OnInteract;
        _getUp.performed -= OnGetUp;
        _move.performed -= OnMove;
        _run.performed -= OnRun;
        _paint.performed -= OnPainting;

        playerInputActions.Clear();
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
            GettingUp?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_move.WasPerformedThisFrame())
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
        if (_look.WasPerformedThisFrame())
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
        if(_run.WasPerformedThisFrame())
        {
            Ran?.Invoke(true);
        }
        else
        {
            Ran?.Invoke(false);
        }
    }

    public void OnPainting(InputAction.CallbackContext context)
    {
        if (_paint.WasPerformedThisFrame())
        {
            Painting?.Invoke();
        }
        if (_paint.WasReleasedThisFrame())
        {
            Painted?.Invoke();
        }
    }
}