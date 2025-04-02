using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEstático : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f; // Rango en el que detecta al jugador
    public float stopChaseRange = 15f; // Rango en el que deja de seguir
    public float stopDistance = 1f; // 🔹 Distancia a la que se detiene antes de llegar al jugador
    public float speed = 3f;

    private Vector3 startPosition;
    private bool isChasing = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else if (distanceToPlayer >= stopChaseRange)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            PerseguirJugador(distanceToPlayer);
        }
        else
        {
            RetornarPosicionInicial();
        }
    }

    void PerseguirJugador(float distanceToPlayer)
    {
        if (distanceToPlayer > stopDistance) // 🔹 Se mueve solo si está más lejos de la distancia de parada
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 targetPosition = player.position - direction * stopDistance; // 🔹 Calcula el punto antes de tocar al jugador

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.LookAt(player);
        }
    }

    void RetornarPosicionInicial()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        transform.LookAt(startPosition);
    }
}