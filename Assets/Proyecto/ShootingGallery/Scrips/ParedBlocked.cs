using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedBlocked : MonoBehaviour
{
    public bool ParedBlockeada = true;
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        LiveFlowers();
    }

    private void LiveFlowers()
    {
        if (ParedBlockeada)
        {
            _collider.enabled = true;  // Collider activado
        }
        else
        {
            _collider.enabled = false; // Collider desactivado
        }
    }

    public void ChangeState(bool Changed)
    {
        ParedBlockeada = Changed;
    }
}

