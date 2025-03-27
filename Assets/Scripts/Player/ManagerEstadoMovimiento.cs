using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoMovimiento : MonoBehaviour
{
    public float VelocidadMovimiento = 3f;

    [HideInInspector] public Vector3 dir;

    CharacterController controller;

    float hzInput, vInput;

    Animator playerAnim;

    public bool _hasPistol = false;

    [SerializeField] float groundYOffset;

    [SerializeField] LayerMask groundMask;

    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;

    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponentInChildren<Animator>();

        _hasPistol = false;
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        AnimMenu();
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
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    public void AnimMenu()
    {
        playerAnim.SetFloat("X", dir.x);
        playerAnim.SetFloat("Y", dir.z);
        playerAnim.SetBool("HoldPistol", _hasPistol);

        if (_hasPistol == true)
        {
            playerAnim.SetLayerWeight(1, 1);
        }
    }
}