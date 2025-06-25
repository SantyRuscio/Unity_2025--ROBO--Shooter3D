using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Item
{
    [Header("Asignaciones")]
    private bool _isPlayerInside;
    private EnemigoIA[] enemigos;
    [SerializeField] private RejasEscape _rejasEscape;
    [SerializeField] private float _timerSound = 0.3f;
    

    [Header("Audio")]
    [SerializeField] private AudioSource _Sonido;
    [SerializeField] private AudioClip _sonidoDiamond;

    private void Start()
    {
        enemigos = FindObjectsOfType<EnemigoIA>();
    }

    protected override bool CanItemBeUse()
    {
        return _isPlayerInside;
    }

    public override void Interactuar()
    {
        foreach (EnemigoIA current in enemigos)
        {
            current.Agresive = true;
        }

        if (_sonidoDiamond != null && _Sonido != null)
            _Sonido.PlayOneShot(_sonidoDiamond);

        _rejasEscape.DiamondWasPick(true);

        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(ReproducirDiamond());

    }
    private IEnumerator ReproducirDiamond()
    {
        yield return new WaitForSeconds(_timerSound);
        Destroy(gameObject);
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayerInside = HasEnter;
        }
    }
}