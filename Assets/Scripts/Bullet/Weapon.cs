using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] private TextMeshProUGUI bulletsText;

    [SerializeField] protected Transform _shootSpawn;

    [SerializeField] private GameObject weaponDropPrefab; // Prefab a soltar cuando se suelta el arma
    [SerializeField] private Transform weaponDropPoint;   // Punto donde aparecerá (en el suelo)


    //Balas que se pueden disparar le puse 500 depues lo editamos mas adelanmte
    [SerializeField] protected int _remainingBullets = 500;

    [SerializeField] protected WeaponSettings _data;

    public abstract void Shoot();
    public abstract void Realease();

    protected abstract bool CheckCanShoot();

    protected virtual void Start()
    {
        if (bulletsText == null)
        {
            bulletsText = GameObject.Find("CantidadDeBalas").GetComponent<TextMeshProUGUI>();
        }

        UpdateBulletsUI();
    }

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

        UpdateBulletsUI(); 

        if (_remainingBullets <= 0)
        {
            Debug.Log("No tenes mas balas");
        }
    }

    private void UpdateBulletsUI()
    {
        if (bulletsText != null)
            bulletsText.text = "" + _remainingBullets;
    }
}