using Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("CharacterController")] 
    [Space] 
    [SerializeField] private CharacterController player;
    
    [Header("Input")] 
    [Space] 
    [SerializeField] private InputReaderData inputReader;
    [SerializeField] private float smoothMoveInputSpeed;
    [SerializeField] private float smoothLookInputSpeed;

    [Header("Movements")] 
    [Space] 
    [SerializeField] private float playerSpeed = 12f;
    [SerializeField] private float runSpeed = 30f;
    [SerializeField] private float stamina = 10f;
    [SerializeField] private float timeToRecover = 5.0f;
    [SerializeField] private float staminaMax;

    [Header("Gravity")] 
    [Space] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.3f;

    [Header("Camera")] 
    [Space] 
    [SerializeField] private Transform playerCam;
    
    [Header("Listen to Event Channels")] 
    [Space] 
    [SerializeField] private VoidEventChannelData changingCamera;
    [SerializeField] private VoidEventChannelData resetTransitionEnded;
    
    private Vector2 _playerLookInput;
    private Vector2 _smoothLookInput;
    private Vector2 _smoothLookCurrentVelocity;
    private Vector2 _playerMoveInput;
    private Vector2 _smoothMoveInput;
    private Vector2 _smoothMoveCurrentVelocity;
    private Vector3 _moveVector;
    private Vector3 _gravityVelocity;
    
    private readonly float _gravity = -9.8f;
    private readonly float _mouseHorizontalSensibility = 0.25f;
    private readonly float _mouseVerticalSensibility = 0.2f;
    private readonly float _minVerticalMouseRotation = -65f;
    private readonly float _maxVerticalMouseRotation = 60f;
    private float _mouseHorizontalInput;
    private float _mouseVerticaInput;

    private bool _enableMovement = true;
    private bool _enableLook = true;
    private bool _isGrounded;
    private bool _isRunning;

    private void Awake()
    {
        player = GetComponent<CharacterController>();

        staminaMax = stamina;
        
        changingCamera.EventRaised += OnChangingCamera;
        resetTransitionEnded.EventRaised += OnResetTransitionEnded;
        inputReader.Moved += OnPlayerMove;
        inputReader.Looked += OnPlayerLook;
        inputReader.Ran += OnPlayerRun;
    }

    private void Update()
    {
        SetGravity();
        Moverse();
        Mirar();
    }
    private void Moverse()
    {
        if (!_enableMovement)
        {
            return;
        }

        _smoothMoveInput = Vector2.SmoothDamp(_smoothMoveInput, _playerMoveInput, ref _smoothMoveCurrentVelocity, smoothMoveInputSpeed);
        _moveVector = new Vector3(_smoothMoveInput.x, 0, _smoothMoveInput.y);
        _moveVector = Vector3.ClampMagnitude(_moveVector, 1);

        if (_isRunning && stamina >= 0f)
        {
            _moveVector = transform.TransformDirection(_moveVector) * runSpeed;

            stamina -= Time.deltaTime;

            if (stamina <= 0f)
            {
                stamina = -timeToRecover;
            }
        }
        else
        {
            _moveVector = transform.TransformDirection(_moveVector) * playerSpeed;

            if (stamina < staminaMax)
            {
                stamina += Time.deltaTime;
            }
        }

        player.Move(_moveVector * Time.deltaTime);
    }

    private void Mirar()
    {
        if (!_enableLook)
        {
            return;
        }

        _smoothLookInput = Vector2.SmoothDamp(_smoothLookInput, _playerLookInput, ref _smoothLookCurrentVelocity, smoothLookInputSpeed);
        
        _mouseHorizontalInput = _mouseHorizontalSensibility * _smoothLookInput.x;
        _mouseVerticaInput += _mouseVerticalSensibility * _smoothLookInput.y;
        
        _mouseVerticaInput = Mathf.Clamp(_mouseVerticaInput, _minVerticalMouseRotation, _maxVerticalMouseRotation);

        playerCam.localEulerAngles = new Vector3(-_mouseVerticaInput, 0, 0);

        transform.Rotate(0, _mouseHorizontalInput, 0);
    }

    private void SetGravity()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _gravityVelocity.y < 0)
        {
            _gravityVelocity.y = -2f;
        }
        
        _gravityVelocity.y += _gravity * Time.deltaTime;
        player.Move(_gravityVelocity * Time.deltaTime);
    }
    private void OnPlayerMove(Vector2 inputVector2)
    {
        _playerMoveInput = inputVector2;
    }
    
    private void OnPlayerLook(Vector2 lookInput)
    {
        _playerLookInput = lookInput;
    }
    private void OnPlayerRun(bool runInputState)
    {
        _isRunning = runInputState;
    }
    private void OnChangingCamera()
    {
        _enableMovement = false;
        _enableLook = false;
    }

    private void OnResetTransitionEnded()
    {
        ResetInputValues();
        _enableMovement = true;
        _enableLook = true;
    }
    
    private void ResetInputValues()
    {
        smoothMoveInputSpeed = 0;
        _moveVector = Vector3.zero;
    }
}