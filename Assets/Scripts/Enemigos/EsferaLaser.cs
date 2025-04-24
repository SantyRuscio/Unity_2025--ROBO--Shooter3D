using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaLaser : EnemigoIA 
{
    [Header("Configuraci�n")]
    public float velocidadRotacion = 5f;
    public GameObject[] lasers;

    private bool _lasersActivados;

    void Start() 
    {
        ActivarLasers(false); 
        _lasersActivados = false;
    }

    void LateUpdate()
    {
        if (AggresiveMode && !_lasersActivados)
        {
            _lasersActivados = true;
            ActivarLasers(true);
        }
        else if (!AggresiveMode && _lasersActivados)
        {
            _lasersActivados = false;
            ActivarLasers(false);
        }

        if (_lasersActivados)
        {
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
        }
    }

    void ActivarLasers(bool activar)
    {
        foreach (GameObject laser in lasers)
        {
            if (laser != null)
                laser.SetActive(activar);
        }
    }
}
