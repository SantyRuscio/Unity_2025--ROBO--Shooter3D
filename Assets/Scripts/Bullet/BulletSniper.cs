using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSniper : MonoBehaviour
{
    Rigidbody bulletRb;

    [SerializeField] float bulletPower = 16f;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] int damageToPlayer = 20; // Cambiado a int para coincidir con PlayerHealth

    private float time = 0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletPower, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bala impactó contra: " + other.gameObject.name);

        // Si la bala colisiona con el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer); // Aplica daño al jugador
            }
        }

        Destroy(gameObject); // La bala se destruye sin importar con qué choque.
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
