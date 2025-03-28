using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movimiento : MonoBehaviour
{

    private Transform playerTr;
    Rigidbody playerRb;
    Animator playerAnim;

    public float playerSpeed;
    private Vector2 newDireccion;

    [SerializeField] private bool _grounded; //esto falta cambiar a FALSE CUANDO este el chequeo de si toca el piso
     
    [SerializeField] private float _jumpForce = 5f;


    void Start()
    {
        playerTr = this.transform;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        KeysMovement();
        AnimMenu();

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            Jump();
        }
    }
    private void KeysMovement()
    {
        Vector3 direction = playerRb.velocity;

       float moveX = Input.GetAxisRaw("Horizontal");
       float moveZ = Input.GetAxisRaw("Vertical");
       float theTime = Time.deltaTime;

        newDireccion = new Vector2 (moveX, moveZ);

        Vector3 side = playerSpeed * moveX * theTime * playerTr.right;
        Vector3 forward = playerSpeed * moveZ * theTime * playerTr.forward;

        Vector3 endDireccion = side + forward;

        playerRb.velocity = endDireccion;

    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void AnimMenu()
    {
        playerAnim.SetFloat("X", newDireccion.x);
        playerAnim.SetFloat("Y", newDireccion.y);
    }

}