using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPlayer : MonoBehaviour
{
    private bool _hasPistol = false;
    private bool _hasRifle = false;
    private bool _hasWeapon = false;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _PickUpRifleSFX;
    [SerializeField] private AudioClip _PickUpPistolSFX;

    [SerializeField] private EnemigoIA[] _enemigos;

    [SerializeField] private Image crosshairImage;

    private Weapon _currentWeapon;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        crosshairImage.enabled = false;
    }

    public Weapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }

    public bool GetHasWeapon()
    {
        return _hasWeapon;
    }

    public bool GetHasPistol()
    {
        return _hasPistol;
    }
    
    public bool GetHasRifle()
    {
        return _hasRifle;
    }

    public void GetWeapon(WeaponType weaponType, GameObject weaponprefab, Transform intanceSlot)
    {
        if (_hasWeapon == true)
        {
            // Hacer logica de reemplazar el arma
        }
        else
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    _hasPistol = true;
                    crosshairImage.enabled = true;
                    _audioSource.PlayOneShot(_PickUpPistolSFX);
                    break;

                case WeaponType.Rifle:
                    _hasRifle = true;
                    crosshairImage.enabled = true;
                    _audioSource.PlayOneShot(_PickUpRifleSFX);
                    break;

                default:
                    break;
            }

            GameObject instantiatedItem = Instantiate(weaponprefab, intanceSlot.position, intanceSlot.rotation);

            instantiatedItem.transform.parent = intanceSlot;

            _currentWeapon = instantiatedItem.GetComponent<Weapon>();

            _hasWeapon = true;

            // enemigoIA.AggresiveMode = true;

            foreach (EnemigoIA current in _enemigos)
            {
                current.Agresive = true;
            }
        }
    }
}