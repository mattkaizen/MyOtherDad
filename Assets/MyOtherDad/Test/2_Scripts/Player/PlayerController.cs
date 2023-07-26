using System.Runtime.CompilerServices;
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
    [SerializeField] private float smoothInputSpeed;

    [Header("Movements")] 
    [Space] 
    [SerializeField] private Vector2 playerInput;
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
    
    private Vector3 _moveVector;
    private Vector3 _gravityVelocity;
    private Vector2 _smoothInput;
    private Vector2 _smoothCurrentVelocity;
    
    private readonly float _gravity = -9.8f;
    private readonly float _mouseHorizontalSensibility = 3f;
    private readonly float _mouseVerticalSensibility = 2f;
    private readonly float _minVerticalMouseRotation = -65f;
    private readonly float _maxVerticalMouseRotation = 60f;
    private float _mouseHorizontalInput;
    private float _mouseVerticaInput;

    private bool _enableMovement = true;
    private bool _enableLook = true;
    private bool _isGrounded;

    private void Awake()
    {
        player = GetComponent<CharacterController>();

        staminaMax = stamina;
        
        changingCamera.EventRaised += OnChangingCamera;
        resetTransitionEnded.EventRaised += OnResetTransitionEnded;
        inputReader.Moved += OnPlayerMove;
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

        _smoothInput = Vector2.SmoothDamp(_smoothInput, playerInput, ref _smoothCurrentVelocity, smoothInputSpeed);
        _moveVector = new Vector3(_smoothInput.x, 0, _smoothInput.y);
        _moveVector = Vector3.ClampMagnitude(_moveVector, 1);

        if (Input.GetKey(KeyCode.LeftShift) && stamina >= 0f)
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

        _mouseHorizontalInput = _mouseHorizontalSensibility * Input.GetAxis("Mouse X");
        _mouseVerticaInput += _mouseVerticalSensibility * Input.GetAxis("Mouse Y");

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
        playerInput = inputVector2;
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
        smoothInputSpeed = 0;
        _moveVector = Vector3.zero;
    }
}