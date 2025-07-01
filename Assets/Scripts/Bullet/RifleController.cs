using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : Weapon
{
    private bool _isShooting;
    [SerializeField] public AudioSource _AkTiro;

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

            InstanciateBullet();
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