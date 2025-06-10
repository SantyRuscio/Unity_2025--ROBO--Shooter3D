using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowersLife : MonoBehaviour
{
    [SerializeField] private ParedBlocked _paredBlocked;

    [SerializeField] private float _timerSound = 0.7f;

    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoMacetas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _paredBlocked.ParedBlockeada = false;
            Debug.Log("Me pegó una bala");

            GetComponent<Collider>().enabled = false;
            StartCoroutine(ReproducirMacetas());
        }
    }

    private IEnumerator ReproducirMacetas()
    {
        _Sonido.PlayOneShot(sonidoMacetas);
        yield return new WaitForSeconds(_timerSound); // espera que el audio termine
        Destroy(gameObject);
    }
}
