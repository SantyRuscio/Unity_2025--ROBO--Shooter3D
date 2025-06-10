using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovAndStamina : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float _playerSpeed = 3f;
    [SerializeField] private float _playerSpeedMultiplier = 1.5f;
    private MovementHandle _movementHandle;
    private CamaraSeguimiento _camaraSeguimiento;
    private bool _canMove = true;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina;
    public float staminaDrain = 20f;
    public float staminaRegen = 10f;
    private bool isSprinting = false;
    private bool isExhausted = false;

    [Header("Jumping")]
    private bool _canJump = false;
    private bool _isJumping = false;
    [SerializeField] private float _jumpForcce = 5f;
    [SerializeField] private float _groundDistanceToJump = 2.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _gravity = -9.81f;
    private Rigidbody _rigidBody;
    private Vector3 _velocity;

    [Header("UI")]
    public Image staminaBarFill;
    private WeaponPlayer _weaponPlayer;

    [Header("CAMARA")]
    [SerializeField] private Transform _cameraAxis;
    [SerializeField] private Transform _cameraTrack;

    [Header("AUDIO")]
    [SerializeField] private AudioSource _sonido;
    [SerializeField] private AudioClip _Salto;

    #region UNITY METHODS
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _weaponPlayer = GetComponent<WeaponPlayer>();

        _movementHandle = new MovementHandle(_playerSpeed, _jumpForcce, transform, _rigidBody);

        _camaraSeguimiento = new CamaraSeguimiento(transform, _cameraTrack, _cameraAxis);
    }

    private void Start()
    {
        stamina = maxStamina;
        UpdateStaminaBar();
    }

    private void Update()
    {
        HandleInput();

        HandleSprint();

        HandleGravity();

        UpdateStaminaBar();
    }

    private void LateUpdate()
    {
        _camaraSeguimiento.CameraLogic(Input.GetAxis("Mouse X") , Input.GetAxis("Mouse Y"));
    }

    #endregion

    #region GETTER METHOD

    public void SetMovementEnabled(bool enabled)
    {
        _canMove = enabled;
    }
 
    public Vector3 GetMovementDirection()
    {
        return _movementHandle.GetDirection();
    }

    private bool IsGrounded()
    {
        bool groundCheck = Physics.Raycast(transform.position, (transform.up * -1), _groundDistanceToJump, _groundMask);

        return groundCheck;
    }
    #endregion


    #region JUMPING METHODS
    public bool GetIsJumping()
    {
        return _isJumping;
    }
    public void ChangeCanJumpState(bool NewState)
    {
        _canJump = NewState;
    }

    private void Jump()
    {
        Debug.Log("Intentando saltar");

        if (IsGrounded())
        {
            Debug.Log("Saltando");
            _sonido.PlayOneShot(_Salto);
            _isJumping = true;
            _movementHandle.OnJump();
            StartCoroutine(jumpTime());
        }
        else
        {
            Debug.Log("No está en el suelo.");
        }
    }
     IEnumerator jumpTime()
     {
        yield return new WaitForSeconds(0.7f);
        _isJumping = false;
     }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (transform.up * -1) * _groundDistanceToJump);
    }

    #region HANDLE METHODS
    private void HandleInput()
    {
        if (!_canMove) return; // bloquear movimiento si está paralizado

        _movementHandle.OnMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (_weaponPlayer.GetHasWeapon())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _weaponPlayer.GetCurrentWeapon().Shoot();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                _weaponPlayer.GetCurrentWeapon().Realease();
            }
        }
    }

    private void HandleGravity()
    {
        if (!IsGrounded())
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = -2;
        }
    }
    void HandleSprint()
    {
        bool isMoving = _movementHandle.IsMoving();

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && isMoving && !isExhausted)
        {
            isSprinting = true;
            _movementHandle.ChangeSpeed(_playerSpeed * _playerSpeedMultiplier);

            stamina -= staminaDrain * Time.deltaTime;

            if (stamina <= 0)
            {
                stamina = 0;
                isExhausted = true;
            }
        }
        else
        {
            isSprinting = false;
            _movementHandle.ChangeSpeed(_playerSpeed);
        }

        if (!isSprinting && stamina < maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;

            if (stamina > 10)
            {
                isExhausted = false;
            }
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    public void ChangeCameraEnableState(bool enable)
    {
        _camaraSeguimiento.ToggleFunctionality(enable);
    }
    #endregion

    void UpdateStaminaBar()
    {
        staminaBarFill.fillAmount = stamina / maxStamina;
    }
}