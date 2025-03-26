using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoMovimiento : MonoBehaviour
{
    public float VelocidadMovimiento = 3f;

    [HideInInspector] public Vector3 dir;

    CharacterController controller;

    float hzInput, vInput;

   [SerializeField] float groundYOffset;

   [SerializeField] LayerMask groundMask;

    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;

    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetDirectionAndMove();

        Gravity();
    }


void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");

        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir * VelocidadMovimiento * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

  
}
