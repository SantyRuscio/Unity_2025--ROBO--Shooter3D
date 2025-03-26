using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody bulletRb;

    [SerializeField] float bulletPower = 16f;

    [SerializeField] float lifeTime = 4;

    private float time = 0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletPower, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CHOCO");
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}