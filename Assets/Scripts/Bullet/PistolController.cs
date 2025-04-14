using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : Weapon
{
    //Tiempo del ultimo disparo
    protected float lastShotTime = 0f;

    public override void Realease() { }

    public override void Shoot()
    {
        if (CheckCanShoot() == false) return;

        lastShotTime = Time.time;  // Actualizar el tiempo del ultimo disparo

       InstanciateBullet();
    }

    protected override bool CheckCanShoot()
    {
        // Solo disparar si quedan balas y ha pasado suficiente tiempo desde el ultimo disparo
        if (_remainingBullets > 0 && Time.time >= lastShotTime + _timeBetweenShots)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}