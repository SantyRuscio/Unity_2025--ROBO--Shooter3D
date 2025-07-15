using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Santiago Ruscio
public class Plataforma : MonoBehaviour, IDamageable
{
    private bool _isFalse = false;

    [Header("Audio")]
    [SerializeField] private AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoPisada;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isFalse)
        {
            _Sonido.PlayOneShot(sonidoPisada);

            RomperPlataforma();
        }
        _Sonido.PlayOneShot(sonidoPisada);
    }
    public void SetIsFalse(bool isfalse)
    {
        _isFalse = isfalse;
    }

    public void TakeDamage(float damage)
    {
        if (_isFalse)
        {
            _Sonido.PlayOneShot(sonidoPisada);
            RomperPlataforma();
        }
    }

    private void RomperPlataforma()
    {
        Debug.Log("�Plataforma falsa, se rompe!");
        Destroy(gameObject);
    }
}