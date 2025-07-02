using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowersLife : MonoBehaviour, IDamageable
{
    [SerializeField] private float vidaPlantas = 1;

    [SerializeField] private ParedBlocked _paredBlocked;

    [SerializeField] private float _timerSound = 0.4f;

    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoMacetas;

    public ParticleSystem explosion;

    private IEnumerator ReproducirMacetas()
    {
        ParticleSystem instanciaExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        instanciaExplosion.Play();

        yield return new WaitForSeconds(_timerSound);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        vidaPlantas -= damage;

        if (vidaPlantas <= 0)
        {
            DestruirFlor();
        }
    }

    private void DestruirFlor()
    {
        _paredBlocked.ParedBlockeada = false;
        Debug.Log("Me pegó una bala");

        GetComponent<Collider>().enabled = false;

        _Sonido.PlayOneShot(sonidoMacetas);

        StartCoroutine(ReproducirMacetas());
    }
}
