using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pildora : Item
{
    private IJump _jugador;

    [SerializeField] private float _timerSound = 0.15f;

    [Header("Audio")]
    [SerializeField] private AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoSalto;

    protected override bool CanItemBeUse()
    {
        return _jugador != null;
    }
        
    public override void Interactuar(GameObject Interactor)
    {
        _jugador = Interactor.GetComponent<IJump>();

        if(_jugador == null) return;

        base.Interactuar(Interactor);

        _jugador.ChangeCanJumpState(true);

        if (sonidoSalto != null && _Sonido != null)
            _Sonido.PlayOneShot(sonidoSalto);

        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(ReproducirSalto());
    }

    private IEnumerator ReproducirSalto()
    {
        yield return new WaitForSeconds(_timerSound);
        Destroy(gameObject);
    }

}