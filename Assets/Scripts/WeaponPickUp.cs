using System;
using System.Collections;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Rifle
};

public class WeaponPickUp : Item
{
    [SerializeField] protected GameObject itemPrefab;
    [SerializeField] protected Transform itemSlot;

    protected MovAndStamina estadoMovimiento;

    [SerializeField] private WeaponType type;

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
        estadoMovimiento.GetWeapon(type, itemPrefab, itemSlot);
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