using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
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

    public void ForceBrocken()
    {
        if (_isFalse)
        {
            RomperPlataforma();
        }
    }

    private void RomperPlataforma()
    {
        Debug.Log("�Plataforma falsa, se rompe!");
        Destroy(gameObject);
    }
}