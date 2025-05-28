using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDoor : MonoBehaviour
{
    [Header("Referencia")]
    [SerializeField] private Transform transformPuerta; // Asigná el objeto que rota

    [Header("Ángulos de apertura")]
    public float doorOpenAngle = -18.9f;
    public float doorCloseAngle = 0f;

    [Header("Velocidad")]
    public float smooth = 3f;

    [Header("Sistema de apertura")]
    public bool doorOpen = false;
    public float activationDistance = 3f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorOpen = !doorOpen; // Alterna entre abrir y cerrar
            }
        }

        // Rotación suave de la puerta
        if (doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transformPuerta.rotation = Quaternion.Slerp(transformPuerta.rotation, targetRotation, smooth * Time.deltaTime);
        }
    }
}