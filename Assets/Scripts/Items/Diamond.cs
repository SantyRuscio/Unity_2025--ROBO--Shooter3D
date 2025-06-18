using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Item
{
    private bool _isPlayerInside;

    private EnemigoIA[] enemigos;

    [SerializeField] private RejasEscape _rejasEscape;

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
            current.AggresiveMode = true;
        }

        _rejasEscape.DiamondWasPick(true);

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