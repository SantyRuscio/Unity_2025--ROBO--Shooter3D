using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitHeal : Item
{
    private PlayerHealth playerLife;

    protected override bool CanItemBeUse()
    {
        if (playerLife == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected override void ItemInteraction()
    {
        if (playerLife.CanRecover())
        {
            RecoverPlayerHealth();
        }
        else
        {
            Debug.Log("El jugador ya tiene la vida completa");
        }
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (HasEnter)
            {
                playerLife = other.gameObject.GetComponent<PlayerHealth>();
            }
            else
            {
                playerLife = null;
            }
        }
    }

    private void RecoverPlayerHealth()
    {
        playerLife.Recover();
        Debug.Log("Jugador curado");
        Destroy(gameObject);
    }
}