using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Bullet : MonoBehaviour
{

    Rigidbody bulletRb;

    [SerializeField] float bulletPower = 50f;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] float _damage = 20f;

    private float time = 0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletPower, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bala impact� contra: " + other.gameObject.name);

        var damageable = other.GetComponent<IDamageable>();

        Plataforma plataforma = other.GetComponent<Plataforma>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }

        if (plataforma != null)
        {
            plataforma.ForceBrocken();
        }
       Destroy(gameObject); // La bala se destruye sin importar con qu� choque. 

    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
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
