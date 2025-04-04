using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemigoIA  EnemigoIA;

    public float maxHealth = 100f;
    private float currentHealth;

    public Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibió daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            EnemigoIA.vivo = false; 
        }
    }
}