using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody bulletRb;

    [SerializeField] float bulletPower = 16f;
    [SerializeField] float lifeTime = 4f;

    private float time = 0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) // Cambiamos a OnCollisionEnter
    {
        Debug.Log("Bala impactó contra: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20f);
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
