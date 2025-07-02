using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Berola Lazaro
public class Medkit : Item
{
    public int healAmount = 30;
    private bool playerInRange = false;
    private IHeal playerHealth;

    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoBotiquin;

    protected override bool CanItemBeUse()
    {
        return playerInRange && playerHealth != null && playerHealth.CanRecover();
    }

    public override void Interactuar(GameObject Interactor)
    {
       base.Interactuar(Interactor);

        playerHealth = Interactor.GetComponent<IHeal>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Debug.Log("Botiquín usado.");
            Destroy(gameObject);
        }
    }
}
