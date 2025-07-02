using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Codigo por: Santiago Ruscio

public class WeaponPlayer : MonoBehaviour
{
    public bool HasWeapon
    {
        get
        {
            return _currentWeapon != null;
        }

        private set { }
    }

    [SerializeField] private Weapon[] weapon;

    private AudioSource _audioSource;

    private EnemigoIA[] _enemigos;

    [SerializeField] private Image crosshairImage;

    private Weapon _currentWeapon;

    Dictionary<WeaponType, Weapon> _weapons = new Dictionary<WeaponType, Weapon>();

    private void Start()
    {
        _enemigos = FindObjectsOfType<EnemigoIA>();

        _audioSource = GetComponent<AudioSource>();
        crosshairImage.enabled = false;

        foreach (var item in weapon)
        {
            _weapons.Add(item.GetweaponType, item);  
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }

    public WeaponType CurrentWeaponType
    {
        get
        {
            return _currentWeapon.GetweaponType;
        }
        
        private set { }
    }

    public void GetWeapon(WeaponType weaponType, GameObject weaponprefab, Transform intanceSlot)
    {
        if (HasWeapon)
        {
            // Hacer logica de reemplazar el arma
        }
        else
        {
            if (_weapons.ContainsKey(weaponType))
            {
                crosshairImage.enabled = true;
                _audioSource.PlayOneShot(_weapons[weaponType].GetPickupSound);
                _currentWeapon = _weapons[weaponType];
            }

            GameObject instantiatedItem = Instantiate(weaponprefab, intanceSlot.position, intanceSlot.rotation);

            instantiatedItem.transform.parent = intanceSlot;

            _currentWeapon = instantiatedItem.GetComponent<Weapon>();

            // enemigoIA.AggresiveMode = true;

            foreach (EnemigoIA current in _enemigos)
            {
                current.Agresive = true;
            }
        }
    }
}