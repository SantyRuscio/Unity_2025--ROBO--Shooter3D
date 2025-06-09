using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConejosSouns : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioDianas;
    [SerializeField] private AudioClip _audioComandante;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _audioSource.PlayOneShot(_audioDianas);
            StartCoroutine(Comandante());
            Debug.Log("Me pegó una bala");
        }
    }

    IEnumerator Comandante()
    {
        yield return new WaitForSeconds(6f);
        _audioSource.PlayOneShot(_audioComandante);
        Debug.Log("Corrutina terminada");
    }
}
