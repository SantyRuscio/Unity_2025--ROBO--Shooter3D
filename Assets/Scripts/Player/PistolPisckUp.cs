using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolPisckUp : Item
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemSlot;
    
    private MovAndStamina estadoMovimiento;

    protected override bool CanItemBeUse()
    {
        if (estadoMovimiento == null)
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
        GameObject instantiatedItem = Instantiate(itemPrefab, itemSlot.position, itemSlot.rotation);
        instantiatedItem.transform.parent = itemSlot;
        estadoMovimiento._hasPistol = true;
        Destroy(gameObject);
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(HasEnter) 
            {
                estadoMovimiento = other.gameObject.GetComponent<MovAndStamina>();
            }
            else
            {
                estadoMovimiento = null;
            }
        }
    }

    
}