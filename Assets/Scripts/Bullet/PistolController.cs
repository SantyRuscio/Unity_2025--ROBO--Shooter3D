using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Berola Lazaro

public class PistolController : Weapon
{
    //Tiempo del ultimo disparo
    protected float lastShotTime = 0f;

    [SerializeField] public AudioSource _Pistol;

    public ParticleSystem fogonazo;
    public override void Realease() { }

    public override void Shoot()
    {
        if (CheckCanShoot() == false) return;

        _Pistol.PlayOneShot(_data.sonidoDisparo);

        fogonazo.Play();

        lastShotTime = Time.time;  // Actualizar el tiempo del ultimo disparo

       InstanciateBullet();
    }

    protected override bool CheckCanShoot()
    {
        // Solo disparar si quedan balas y ha pasado suficiente tiempo desde el ultimo disparo
        if (_remainingBullets > 0 && Time.time >= lastShotTime + _data._timeBetweenShots)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}