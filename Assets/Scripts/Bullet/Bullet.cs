using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//Codigo por: Berola Lazaro

public class Bullet : MonoBehaviour
{

    Rigidbody bulletRb;

    [SerializeField] private GameObject hitParticlesPrefab; 
    [SerializeField] float bulletPower = 50f;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] float _damage = 20f;

    private float time = 0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletPower, ForceMode.Impulse);

        Collider checkerCollider = GameObject.Find("Checker").GetComponent<Collider>();
        Collider myCollider = GetComponent<Collider>();

        if (checkerCollider != null && myCollider != null)
        {
            Physics.IgnoreCollision(myCollider, checkerCollider);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bala impact� contra: " + other.gameObject.name);

        if (other.GetComponent<CollisionDetecter>() != null) { }

        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }

        Debug.Log(other.gameObject.name);
       Destroy(gameObject); 
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
