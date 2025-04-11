using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform _shootSpawn;

    public bool _shooting = false;

    public GameObject bulletPrefab;

    private int remainingBullets = 500;  //  balas que se pueden disparar le puse 500 depues lo editamos mas adelanmte
    private float timeBetweenShots = 0.5f;  // Tiempo de cooldown entre disparos 
    private float lastShotTime = 0f;  //  tiempo del �ltimo disparo

    void Update()
    {
        // Solo disparar si quedan balas y ha pasado suficiente tiempo desde el �ltimo disparo
        if (remainingBullets > 0 && Time.time >= lastShotTime + timeBetweenShots && Input.GetButtonDown("Fire1"))
        {
            Shoot();
            lastShotTime = Time.time;  // Actualizar el tiempo del �ltimo disparo
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 shootDirection;
        if (Physics.Raycast(ray, out hit))
        {
            shootDirection = (hit.point - _shootSpawn.position).normalized;
        }
        else
        {
            shootDirection = Camera.main.transform.forward;
        }

        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, _shootSpawn.position, Quaternion.LookRotation(shootDirection));

        // resta las balas restantes
        remainingBullets--;

        // Mostrar las balas restantes en consola checkeo
        Debug.Log("Balas restantes: " + remainingBullets);

        // Si no quedan balas 
        if (remainingBullets <= 0)
        {
            Debug.Log("No tienes m�s balas!");
        }
    }
}