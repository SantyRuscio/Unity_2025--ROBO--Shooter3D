using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Item 
{
    public int healAmount = 30;
    private bool playerInRange = false;
    private PlayerHealth playerHealth;

    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoBotiquin;

    protected override bool CanItemBeUse()
    {
        return playerInRange && playerHealth != null && playerHealth.CanRecover();
    }

    public override void Interactuar()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Debug.Log("Botiquín usado. Vida actual: " + playerHealth.currentHealth);
            Destroy(gameObject);
        }
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.CompareTag("Player"))
        {
            if (HasEnter)
            {
                playerHealth = other.GetComponent<PlayerHealth>();
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
                playerHealth = null;
            }
        }
    }
}