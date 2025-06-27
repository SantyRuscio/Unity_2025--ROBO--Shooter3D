using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform _shootSpawn;

    [SerializeField] protected GameObject _bulletPrefab;

    [SerializeField] public  AudioClip _pickUpSFX;

    //Balas que se pueden disparar le puse 500 depues lo editamos mas adelanmte
    [SerializeField] protected int _remainingBullets = 500;

    //Tiempo de cooldown entre disparos
    [SerializeField] protected float _timeBetweenShots = 0.5f;

    public WeaponType type;
    public abstract void Shoot();
    public abstract void Realease();

    protected abstract bool CheckCanShoot();

    protected void InstanciateBullet()
    {
        // Crear la bala
        Instantiate(_bulletPrefab, _shootSpawn.position, Quaternion.LookRotation(GetShootDirection()));


        RemoveBullet();
    }

    private Vector3 GetShootDirection()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            return (hit.point - _shootSpawn.position).normalized;
        }
        else
        {
            return Camera.main.transform.forward;
        }
    }


    protected void RemoveBullet()
    {
        // resta las balas restantes
        _remainingBullets--;

        // Mostrar las balas restantes en consola checkeo
        Debug.Log("Balas restantes: " + _remainingBullets);

        // Si no quedan balas 
        if (_remainingBullets <= 0)
        {
            Debug.Log("No tienes mas balas!");
        }
    }

}