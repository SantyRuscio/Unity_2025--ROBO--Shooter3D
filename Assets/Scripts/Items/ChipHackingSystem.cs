using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChipHackingSystem : Item
{
    private MovAndStamina _jugador;

    protected override bool CanItemBeUse()
    {
        return _jugador != null;
    }

    protected override void ItemInteraction()
    {
        PlayerAnimator playerAnimator = _jugador.GetComponent<PlayerAnimator>();
        if (playerAnimator != null)
        {
            playerAnimator.TriggerSpecialAnimation();
        }
        Debug.Log("ChipHackingSystem activado");

        // PROXIMAMENTE CAMBIARA LAS CAMARAS , LAS DESACTIVA

        Destroy(gameObject); // Destruir el objeto si querés que desaparezca luego de usarlo
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.CompareTag("Player"))
        {
            if (HasEnter)
            {
                _jugador = other.gameObject.GetComponent<MovAndStamina>();
                Debug.Log("Jugador cerca del ChipHackingSystem");
            }
            else
            {
                _jugador = null;
            }
        }
    }
}
