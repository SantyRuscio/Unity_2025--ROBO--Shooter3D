using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Santiago Ruscio

[System.Serializable]   
public struct WeaponSettings
{
    public GameObject _bulletPrefab;
    public AudioClip _pickUpSFX;
    public AudioClip sonidoDisparo;
    public WeaponType type;
    public float _timeBetweenShots;
}



public abstract class Weapon : MonoBehaviour
{
    public delegate void OnAmmoChanged();

    public event OnAmmoChanged AmmoChanged;

    [SerializeField] protected Transform _shootSpawn;

    //Balas que se pueden disparar le puse 500 depues lo editamos mas adelanmte
    [SerializeField] protected int _remainingBullets = 500;

    [SerializeField] protected WeaponSettings _data;

    public abstract void Shoot();
    public abstract void Realease();

    protected abstract bool CheckCanShoot();

    public WeaponType GetweaponType
    {
        get { return _data.type; }
    }
    public AudioClip GetPickupSound
    {
        get { return _data._pickUpSFX; }
    }
    protected void RaycastShoot(float Damage)
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f); 
            Debug.Log("Le diste a: " + hit.collider.name);

            // Verificar si tiene vida
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(Damage); 
            }

        }

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
        _remainingBullets--;

        Debug.Log("Balas restantes: " + _remainingBullets);

        AmmoChanged?.Invoke(); 

        if (_remainingBullets <= 0)
        {
            Debug.Log("No tenes mas balas");
        }
    }

    public int GetRemainingBullets()
    {
        return _remainingBullets;
    }
}