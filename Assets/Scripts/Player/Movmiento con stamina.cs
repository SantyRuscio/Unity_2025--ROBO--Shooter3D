using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovAndStamina : MonoBehaviour
{
    [Header("Movimiento")]
    public float VelocidadMovimiento = 3f;

    public float MultVelocidad = 1.5f;

    private float playerSpeed;

    [Header("Stamina")]

    public float maxStamina = 100f;

    public float stamina;


    public float staminaDrain = 20f;

    public float staminaRegen = 10f;

    private bool isSprinting = false;

    private bool isExhausted = false;

    [HideInInspector] public Vector3 dir;

    //CharacterController controller;
    float hzInput, vInput;

    Animator playerAnim;

    public bool _hasPistol = false;
    public bool _hasRifle = false;
    public bool _hasWeapon = false;
    private Weapon _currentWeapon;

    [SerializeField] float groundYOffset;

    [SerializeField] LayerMask groundMask;

    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;

    Vector3 velocity;

    [SerializeField] public AudioSource _PickUp;
    [SerializeField] private AudioClip PpAk47;
    [SerializeField] private AudioClip PpM11;

    public Image staminaBarFill;

    //public EnemigoIA enemigoIA;

    public Rigidbody rb;

    [SerializeField] private EnemigoIA[] enemigos;

    void Start()
    {
        rb  = GetComponent<Rigidbody>();

        playerAnim = GetComponentInChildren<Animator>();

        stamina = maxStamina;

        playerSpeed = VelocidadMovimiento;

        UpdateStaminaBar();
    }

    void Update()
    {

        GetDirectionAndMove();

        HandleSprint();

        Gravity();

        UpdateStaminaBar();

        if (_hasWeapon)
        {
            if (Input.GetButtonDown("Fire1"))
                _currentWeapon.Shoot();

            if(Input.GetButtonUp("Fire1"))
                _currentWeapon.Realease();
        }
    }

    public void GetWeapon(WeaponType weaponType, GameObject weaponprefab, Transform intanceSlot)
    {
        if(_hasWeapon == true)
        {
            // Hacer logica de reemplazar el arma
        }
        else
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    _hasPistol = true;
                    _PickUp.PlayOneShot(PpM11);
                    break;

                case WeaponType.Rifle:
                    _hasRifle = true;
                    _PickUp.PlayOneShot(PpAk47);
                    break;

                default:
                    break;
            }

            GameObject instantiatedItem = Instantiate(weaponprefab, intanceSlot.position, intanceSlot.rotation);

            instantiatedItem.transform.parent = intanceSlot;

            _currentWeapon = instantiatedItem.GetComponent<Weapon>();

            _hasWeapon = true;

            // enemigoIA.AggresiveMode = true;

            foreach (EnemigoIA current in enemigos)
            {
                current.AggresiveMode = true;
            }
        }
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        Vector3 direc = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        dir = (transform.forward * vInput + transform.right * hzInput).normalized;

        transform.localEulerAngles = direc;
        // rb.MovePosition (rb.position + dir * playerSpeed * Time.deltaTime) ;

        transform.position += dir * playerSpeed * Time.deltaTime;

    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
     //  if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

       // controller.Move(velocity * Time.deltaTime);
    }

    void HandleSprint()
    {
        bool isMoving = hzInput != 0 || vInput != 0;


        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && isMoving && !isExhausted)
        {
            isSprinting = true;
            playerSpeed = VelocidadMovimiento * MultVelocidad;

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
            playerSpeed = VelocidadMovimiento;
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


    void UpdateStaminaBar()
    {

        staminaBarFill.fillAmount = stamina / maxStamina;
    }
}