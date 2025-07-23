using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Beghin Ulises

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public EnemigoIA  EnemigoIA;

    public float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] ParticleSystem sangre;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        Debug.Log("Enemigo " + gameObject.name + " recibio dano. Vida restante: " + currentHealth);

        Particulas();

        if (currentHealth <= 0)
        {
            Debug.Log("TENGO QUE ESTAR MEURTO");
            EnemigoIA.vivo = false; 
        }
    }

    private void Particulas()
    {
        Debug.Log("Ejecutando particulas");

        sangre.Play();

        Debug.Log("Terminaron las particulas");
    }
}