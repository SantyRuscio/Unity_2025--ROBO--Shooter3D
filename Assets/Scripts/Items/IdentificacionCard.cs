using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class IdentificacionCard : Item
{
    private MovAndStamina _player1;
    [SerializeField] private DoorDiamond _doorDiamond;
    [SerializeField] private float _timerSound = 0.3f;


    [Header("Audio")]
    [SerializeField] private AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoIdentifiacion;

    private bool CardPicked = true;
    protected override bool CanItemBeUse()
    {
        return _player1 != null;
    }

    public override void Interactuar(GameObject Interactor)
    {
        base.Interactuar(Interactor);
        Debug.Log("Agarre la card");
        _doorDiamond.CardPicked(CardPicked);

        if (sonidoIdentifiacion != null && _Sonido != null)
            _Sonido.PlayOneShot(sonidoIdentifiacion);

        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(ReproducirIdentificacion());

    }
    private IEnumerator ReproducirIdentificacion()
    {
        yield return new WaitForSeconds(_timerSound);
        Destroy(gameObject);
    }

}
