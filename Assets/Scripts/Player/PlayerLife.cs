using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Configuración de Vida")]

    public float maxHealth = 100;
    public float currentHealth;
    public bool isDead = false;

    [Header("UI")]
    public Image vidaImagen; 

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10); // daño de prueba

            Debug.Log("Vida actual: " + currentHealth);
        }

        // curacion de prueba
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);

            Debug.Log("Vida actual: " + currentHealth);
        }
    } //Daños y curaciones de prueba

    public bool CanRecover()
    {
        return currentHealth < maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if (isDead) return; 

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); 

        UpdateHealthUI();

        //chequear si murio
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //curarse
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        currentHealth = Mathf.Min(currentHealth, maxHealth);

        UpdateHealthUI();
    }

    public bool Heal() //del botiquin
    {

        return currentHealth < maxHealth;
    }

    public void Recover()
    {
        if (isDead) return;

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    //metodo para morir
    void Die()
    {
        isDead = true;
        
        var moveandStamina = GetComponent<MovAndStamina>(); //desactiva el movimiento

        moveandStamina.ChangeCameraEnableState(false);//desactiva la camara
        moveandStamina.enabled = false;

        RestartLevel();
    }

    // reiniciar el nivel 
    void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateHealthUI()
    {
        if (vidaImagen != null)
            vidaImagen.fillAmount = (float)currentHealth / maxHealth; 

    }
}