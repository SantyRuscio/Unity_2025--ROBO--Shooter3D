using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoMovimiento : MonoBehaviour
{
    public float VelocidadMovimiento = 3f;

    [HideInInspector] public Vector3 dir;
    private CharacterController controller;
    private float hzInput, vInput;
    private Vector3 spherePos;
    private Vector3 velocity;

    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;

    private AnimacionesJugador animacionesJugador; // Referencia al nuevo script de animación

    public bool _hasPistol = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animacionesJugador = GetComponent<AnimacionesJugador>(); // Obtener referencia al script de animación
        _hasPistol = false;
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        if (animacionesJugador != null)
        {
            animacionesJugador.ActualizarAnimacion(dir, _hasPistol); // Llamamos a la animación desde el otro script
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

        controller.Move(dir * VelocidadMovimiento * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        return Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask);
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }
}
