using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("CharacterController")] 
    [Space] 
    [SerializeField] private CharacterController player;

    [Header("Movimientos")] 
    [Space] 
    [SerializeField] private float horizontalMove;
    [SerializeField] private float verticalMove;
    [SerializeField] private float playerSpeed = 12f;
    [SerializeField] private float runSpeed = 30f;
    [SerializeField] private float stamina = 10f;
    [SerializeField] private float timeToRecover = 5.0f;
    [SerializeField] private float staminaMax;

    [Header("Gravedad")] 
    [Space] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.3f;

    [Header("Camara")] 
    [Space] 
    [SerializeField] private Transform playerCam;

    private Vector3 _playerInput;
    private Vector3 _gravityVelocity;
    
    private readonly float _gravity = -9.8f;
    private readonly float _mouseHorizontalSensibility = 3f;
    private readonly float _mouseVerticalSensibility = 2f;
    private readonly float _minVerticalMouseRotation = -65f;
    private readonly float _maxVerticalMouseRotation = 60f;
    private float _mouseHorizontalInput;
    private float _mouseVerticaInput;

    private bool _isGrounded;
    private bool _enableMovement;

    Vector3 xyz;

    void Awake()
    {
        player = GetComponent<CharacterController>();

        staminaMax = stamina;
        _enableMovement = true;
    }

    void Update()
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

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        _playerInput = new Vector3(horizontalMove, 0, verticalMove);
        _playerInput = Vector3.ClampMagnitude(_playerInput, 1);

        if (Input.GetKey(KeyCode.LeftShift) && stamina >= 0f)
        {
            xyz = new Vector3(25, 0, 0);
            if (Flashlight != null)
                Flashlight.localEulerAngles = Vector3.Lerp(Flashlight.localEulerAngles, xyz, Time.deltaTime * 2.5f);

            _playerInput = transform.TransformDirection(_playerInput) * runSpeed;

            stamina -= Time.deltaTime;

            if (stamina <= 0f)
            {
                stamina = -timeToRecover;
            }
        }
        else
        {
            xyz = new Vector3(0, 0, 0);
            if (Flashlight != null)
                Flashlight.localEulerAngles = Vector3.Lerp(Flashlight.localEulerAngles, xyz, Time.deltaTime * 2.5f);

            _playerInput = transform.TransformDirection(_playerInput) * playerSpeed;

            if (stamina < staminaMax)
            {
                stamina += Time.deltaTime;
            }
        }

        player.Move(_playerInput * Time.deltaTime);
    }

    private void Mirar()
    {
        if (!_enableMovement)
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
}