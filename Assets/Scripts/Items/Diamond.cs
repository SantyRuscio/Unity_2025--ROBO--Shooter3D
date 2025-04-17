using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Item
{
    [SerializeField] private EnemigoIA[] enemigos;

    protected override bool CanItemBeUse()
    {
        return true;
    }

    protected override void ItemInteraction()
    {
        foreach (EnemigoIA current in enemigos)
        {
            current.naziMode = true; 
        }

        Destroy(gameObject);
    }

    protected override void ItemUpdate(Collider other, bool HasEnter) {}
}
