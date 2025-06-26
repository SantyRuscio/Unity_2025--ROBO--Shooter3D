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
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemSlot;

    private WeaponPlayer _weaponPlayer;

    private EnemigoIA[] enemigos;

    [SerializeField] private WeaponType type;
    private void Start()
    {
        enemigos = FindObjectsOfType<EnemigoIA>();
    }
    protected override bool CanItemBeUse()
    {
        if (_weaponPlayer == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void Interactuar()
    {
        base.Interactuar();
        foreach (EnemigoIA current in enemigos)
        {
            current.Agresive = true;
        }
        _weaponPlayer.GetWeapon(type, itemPrefab, itemSlot);
        Destroy(gameObject);
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(HasEnter) 
            {
                _weaponPlayer = other.gameObject.GetComponent<WeaponPlayer>();
            }
            else
            {
                _weaponPlayer = null;
            }
        }
    }
}