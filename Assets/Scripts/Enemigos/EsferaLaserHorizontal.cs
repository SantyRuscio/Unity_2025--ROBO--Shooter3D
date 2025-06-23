using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaLaserHorizontal : EnemigoIA
{
    [Header("Configuración")]
    public float velocidadMovimiento = 3f;
    public float distanciaHorizontal = 5f;
    public GameObject[] lasers;

    private bool _lasersActivados;
    private Vector3 _posicionInicial;
    private float _direccion = 1f;

    void Start()
    {
        ActivarLasers(false);
        _lasersActivados = false;
        _posicionInicial = transform.position;
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
            transform.Translate(Vector3.right * velocidadMovimiento * _direccion * Time.deltaTime);

            if (transform.position.x > _posicionInicial.x + distanciaHorizontal)
            {
                _direccion = -1f;
            }
            else if (transform.position.x < _posicionInicial.x - distanciaHorizontal)
            {
                _direccion = 1f;
            }
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
