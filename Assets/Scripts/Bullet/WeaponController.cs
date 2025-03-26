using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform _shootSpawn;

    public bool _shooting = false;

    public GameObject bulletPrefab;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

     void Shoot()
     {
        Instantiate(bulletPrefab, _shootSpawn.position, _shootSpawn.rotation);
     }
}