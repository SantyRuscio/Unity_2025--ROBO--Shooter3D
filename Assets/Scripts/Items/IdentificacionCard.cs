using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class IdentificacionCard : Item
{
    private MovAndStamina _player1;
    [SerializeField] private DoorDiamond _doorDiamond;

    private bool CardPicked = true;
    protected override bool CanItemBeUse()
    {
        return _player1 != null;
    }

    public override void Interactuar()
    {
        Debug.Log("Agarre la card");
        _doorDiamond.CardPicked(CardPicked);
        Destroy(gameObject);
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.CompareTag("Player"))
        {
            if (HasEnter)
            {
                _player1 = other.gameObject.GetComponent<MovAndStamina>();
                Debug.Log("ENTRE");
            }
            else
            {
                _player1 = null;
            }
        }
    }
}
