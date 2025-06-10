using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pildora : Item
{
    private MovAndStamina _jugador;

    [SerializeField] private float _timerSound = 0.15f;

    [Header("Audio")]
    [SerializeField] private AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoSalto;

    protected override bool CanItemBeUse()
    {
        return _jugador != null;
    }

    public override void Interactuar()
    {
        _jugador.ChangeCanJumpState(true);
        Debug.Log("oper");

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

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.CompareTag("Player"))
        {
            _jugador = HasEnter ? other.GetComponent<MovAndStamina>() : null;

            if (HasEnter)
                Debug.Log("ENTRE");
        }
    }
}