using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseVideo : MonoBehaviour
{
    bool isPP;

    private Transform playerTr;
    Rigidbody playerRb;
    Animator playerAnim;

    public float playerSpeed;
    private Vector2 newDireccion;

    [SerializeField] private bool _grounded = true; //esto falta cambiar a FALSE CUANDO este el chequeo de si toca el piso
    [SerializeField] Transform cameraAxis;
    [SerializeField] Transform cameraTrack;
    [SerializeField] Transform PP;
    [SerializeField] Transform initialCameraPos;
    private Transform theCamera;
        
    private float rotY = 0f;
    private float rotX = 0f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float camRotSpeed = 200f;
    [SerializeField] private float minAngle = -45f;
    [SerializeField] private float maxAngle = 45f;
    private float cameraSpeed = 1500;
    [SerializeField] private float sens = 2f; // Sensibilidad ajustada

    void Start()
    {
        playerTr = this.transform;
        playerRb = GetComponent<Rigidbody>();
        theCamera = Camera.main.transform;
        playerAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        KeysMovement();
        MouseMovement();
        AnimMenu();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(isPP)
            {
                cameraTrack.position = initialCameraPos.position;
            }
            else
            {
                cameraTrack.position = PP.position;
            }

            isPP = !isPP;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true) // && => si esta en el piso es verdadero entonces entra  
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
    private void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens;
        float mouseY = Input.GetAxis("Mouse Y") * sens;

        // Rotar jugador horizontalmente
        playerTr.Rotate(0, mouseX * camRotSpeed * Time.deltaTime, 0);

        // Control de rotación vertical con límites
        rotY -= mouseY * camRotSpeed * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);
        cameraAxis.localRotation = Quaternion.Euler(rotY, 0, 0);

        // Movimiento y rotación de la cámara con velocidad normal
        
        theCamera.position = Vector3.MoveTowards(theCamera.position, cameraTrack.position, cameraSpeed * Time.deltaTime); // ponr a la camara siempre en la posicion tracker

        theCamera.rotation = Quaternion.RotateTowards(theCamera.rotation, cameraTrack.rotation, cameraSpeed * Time.deltaTime); // pone la camara siomepre en la rotaion del treacker
    }

    void Jump()
    {
        playerRb.AddForce(transform.up * _jumpForce, ForceMode.Impulse); //falta chequear el piso 
    }

    public void AnimMenu()
    {
        playerAnim.SetFloat("X", newDireccion.x);
        playerAnim.SetFloat("Y", newDireccion.y);
    }
}