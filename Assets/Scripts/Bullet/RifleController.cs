using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : Weapon
{
    private bool _isShooting;
    [SerializeField] public AudioSource _AkTiro;
    [SerializeField] private AudioClip sonidoDisparo;

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
            _AkTiro.PlayOneShot(sonidoDisparo);
            yield return new WaitForSeconds(_timeBetweenShots);

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