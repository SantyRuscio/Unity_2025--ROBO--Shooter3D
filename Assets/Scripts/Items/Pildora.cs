using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pildora : Item
{
    private MovAndStamina _jugador;

    protected override bool CanItemBeUse()
    {
        return _jugador != null; 
    }

    protected override void ItemInteraction()
    {
        _jugador.ChangeCanJumpState(true);
        Debug.Log("oper");
        Destroy(gameObject); 
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.CompareTag("Player"))
        {
            if (HasEnter)
            {
                _jugador = other.gameObject.GetComponent<MovAndStamina>();
                Debug.Log("ENTRE");
            }
            else
            {
                _jugador = null;
            }
        }
    }

    public interface Iinteractuables
    {
        void Interactuar();
    }
}