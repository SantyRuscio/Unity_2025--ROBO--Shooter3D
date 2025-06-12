using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowersLife : MonoBehaviour
{
    [SerializeField] private ParedBlocked _paredBlocked;

    [SerializeField] private float _timerSound = 0.4f;

    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoMacetas;

    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _paredBlocked.ParedBlockeada = false;
            Debug.Log("Me peg� una bala");

            GetComponent<Collider>().enabled = false;

            _Sonido.PlayOneShot(sonidoMacetas);

            StartCoroutine(ReproducirMacetas());
        }
    }

    private IEnumerator ReproducirMacetas()
    {

        ParticleSystem instanciaExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        instanciaExplosion.Play();

        yield return new WaitForSeconds(_timerSound);
        Destroy(gameObject);
    }
}
