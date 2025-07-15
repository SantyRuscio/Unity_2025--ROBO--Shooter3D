using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Santiago Ruscio

public class ElectricBall : MonoBehaviour
{
    Rigidbody bulletRb;

    [SerializeField] float _bulletPower = 50f;
    [SerializeField] float _lifeTime = 10f;
    [SerializeField] float _damage = 10f;
    [SerializeField] float _paralyzeTime = 1.5f;

    [SerializeField]  private float time = 0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * _bulletPower, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bala impactó contra: " + other.gameObject.name);

        var damageable = other.GetComponent<IParalyze>();

        if (damageable != null)
        {
            damageable.TakeDamageParalyzed(_damage);

        }
    }


    private void Update()
    {
        time += Time.deltaTime;
        if (time > _lifeTime)
        {
            Destroy(gameObject);
        }
    }

}