using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Ruscio Santiago

public class LaserDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null && other.CompareTag("Player"))
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}
