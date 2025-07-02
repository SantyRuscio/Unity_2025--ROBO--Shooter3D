using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConejosSouns : MonoBehaviour, IDamageable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioDianas;

    [SerializeField] private AudioSource _audioSourceComandante;
    [SerializeField] private AudioClip _audioComandante;

    private bool _audioComandanteReproducido = false;

    public void TakeDamage(float damage)
    {

        Debug.Log("Me pego una bala" + "me hizo" + damage + "daño, pero no me muero por que estoy re potente");

        _audioSource.PlayOneShot(_audioDianas);

        if (!_audioComandanteReproducido)
        {
            _audioComandanteReproducido = true;
            StartCoroutine(Comandante());
        }
    }

    IEnumerator Comandante()
    {
        yield return new WaitForSeconds(6f);
        _audioSourceComandante.PlayOneShot(_audioComandante);
        Debug.Log("Corrutina terminada");
    }
}
