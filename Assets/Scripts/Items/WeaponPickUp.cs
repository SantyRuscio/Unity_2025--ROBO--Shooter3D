using System;
using System.Collections;
using UnityEngine;

//Codigo por: Berola Lazaro

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

    public override void Interactuar(GameObject Interactor)
    {
        base.Interactuar(Interactor);

        _weaponPlayer = Interactor.GetComponent<WeaponPlayer>();

        if (_weaponPlayer == null) return;

        foreach (EnemigoIA current in enemigos)
        {
            current.Agresive = true;
        }
        _weaponPlayer.GetWeapon(type, itemPrefab, itemSlot);
        Destroy(gameObject);
    }
}