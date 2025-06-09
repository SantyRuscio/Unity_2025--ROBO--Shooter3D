using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConejosSouns : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioDianas;

    [SerializeField] private AudioSource _audioSourceComandante;
    [SerializeField] private AudioClip _audioComandante;

    private bool _audioComandanteReproducido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _audioSource.PlayOneShot(_audioDianas);
            Debug.Log("Me pegó una bala");

            if (!_audioComandanteReproducido)
            {
                _audioComandanteReproducido = true;
                StartCoroutine(Comandante());
            }
        }
    }

    IEnumerator Comandante()
    {
        yield return new WaitForSeconds(6f);
        _audioSourceComandante.PlayOneShot(_audioComandante);
        Debug.Log("Corrutina terminada");
    }
}
