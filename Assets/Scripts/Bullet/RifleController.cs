using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Beghin Ulises

public class RifleController : Weapon
{
    private bool _isShooting;
    [SerializeField] public AudioSource _AkTiro;
    [SerializeField] private float AkDamage = 30f;

    public override void Realease()
    {
        _isShooting = false;
        StopCoroutine(Shooting());
    }

    public override void Shoot()
    {
        if(CheckCanShoot() == false) return;

        _isShooting = true;

        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        while (_isShooting)
        {
            _AkTiro.PlayOneShot(_data.sonidoDisparo);
            yield return new WaitForSeconds(_data._timeBetweenShots);

            RaycastShoot(AkDamage);
        }
    }

    protected override bool CheckCanShoot()
    {
        if(_remainingBullets > 0)
            return true;
        else 
            return false;
    }
}