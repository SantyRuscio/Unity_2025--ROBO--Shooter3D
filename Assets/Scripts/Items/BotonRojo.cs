using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Ruscio Santiago
public class BotonRojo : Item
{
    [Header("Asignaciones")]
    private MovAndStamina _jugador;
    [SerializeField] private Transform _Reja;
    [SerializeField] private CapsuleCollider _collider;


    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoBoton;
    [SerializeField] private AudioClip _sonidoReja;

    [Header("Valores")]
    [SerializeField] private float _alturaFinal = 15f;
    [SerializeField] private float _velocidadMovimiento = 2f;

    private bool _rejaSubiendo = false;

    protected override bool CanItemBeUse()
    {
        return _jugador != null;
    }

    public override void Interactuar(GameObject Interactor)
    {
        base.Interactuar(Interactor);

        if (!_rejaSubiendo)
        {
            _Sonido.PlayOneShot(sonidoBoton);
            StartCoroutine(SubirRejaSuavemente());
            ToggleShowTag(false);
            _collider.enabled = false;

        }
    }

    private IEnumerator SubirRejaSuavemente()
    {
        _rejaSubiendo = true;
        _Sonido.PlayOneShot(_sonidoReja);
        Vector3 posicionInicial = _Reja.position;
        Vector3 posicionFinal = new Vector3(posicionInicial.x, _alturaFinal, posicionInicial.z);

        while (Vector3.Distance(_Reja.position, posicionFinal) > 0.01f)
        {
            _Reja.position = Vector3.MoveTowards(_Reja.position, posicionFinal, _velocidadMovimiento * Time.deltaTime);
            yield return null;
        }

        _Reja.position = posicionFinal;
    }
}

