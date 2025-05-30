using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public EnemigoIA  EnemigoIA;

    public float maxHealth = 100f;
    private float currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo " + gameObject.name + " recibio dano. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("TENGO QUE ESTAR MEURTO");
            EnemigoIA.vivo = false; 
        }
    }
}