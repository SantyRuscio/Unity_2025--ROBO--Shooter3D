using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Berola Lazaro
public abstract class LaserPadre : EnemigoIA
{
    [Header("Configuración General")]
    public GameObject[] lasers;
    protected bool _lasersActivados;

    protected virtual void Start()
    {
        ActivarLasers(false);
        _lasersActivados = false;
    }

    protected virtual void LateUpdate()
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
            EjecutarMovimiento();
        }
    }

    protected abstract void EjecutarMovimiento();

    protected void ActivarLasers(bool activar)
    {
        foreach (GameObject laser in lasers)
        {
            if (laser != null)
                laser.SetActive(activar);
        }
    }
}
