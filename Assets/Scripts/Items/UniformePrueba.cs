using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

//Codigo por: Beghin Ulises

public class UniformePrueba : Item
{
    private MovAndStamina _player1;
    [SerializeField] private float _timerSound = 0.3f;


    [Header("Audio")]
    [SerializeField] private AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoRopa;

    protected override bool CanItemBeUse()
    {
        return _player1 != null;
    }

    public override void Interactuar(GameObject Interactor)
    {
        base.Interactuar(Interactor);
        Debug.Log("Agarre el uniforme");

        if (sonidoRopa != null && _Sonido != null)
            _Sonido.PlayOneShot(sonidoRopa);

        GetComponent<Collider>().enabled = false;
       // GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(ReproducirIdentificacion());

    }
    private IEnumerator ReproducirIdentificacion()
    {
        yield return new WaitForSeconds(_timerSound);
        Destroy(gameObject);
    }

}
