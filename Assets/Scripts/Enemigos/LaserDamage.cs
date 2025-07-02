using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Ruscio Santiago

public class LaserDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bala impact� contra: " + other.gameObject.name);

        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null && other.CompareTag("Player"))
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}
