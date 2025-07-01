using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoMovimientooS
{ 
    Vertical,
    Horizontal, 
    Rotacion 
}

public class EsperaLaser : EnemigoIA
{

    [Header("Configuración general")]
    [SerializeField] private TipoMovimientooS tipoMovimiento;
    [SerializeField] float velocidadMovimiento;
    [SerializeField] float distanciaMovimiento;
    [SerializeField] float velocidadRotacion;
    [SerializeField] GameObject[] lasers;

    private bool _lasersActivados;
    private Vector3 _posicionInicial;
    private float _direccion = 1f;

    void Start()
    {
        _posicionInicial = transform.position;
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

        if (!_lasersActivados) return;

        if (tipoMovimiento == TipoMovimientooS.Vertical)
        {
            transform.Translate(Vector3.up * velocidadMovimiento * _direccion * Time.deltaTime);

            float desplazamiento = transform.position.y - _posicionInicial.y;
            if (desplazamiento > distanciaMovimiento) _direccion = -1f;
            else if (desplazamiento < -distanciaMovimiento) _direccion = 1f;
        }
        else if (tipoMovimiento == TipoMovimientooS.Horizontal)
        {
            transform.Translate(Vector3.right * velocidadMovimiento * _direccion * Time.deltaTime);

            float desplazamiento = transform.position.x - _posicionInicial.x;
            if (desplazamiento > distanciaMovimiento) _direccion = -1f;
            else if (desplazamiento < -distanciaMovimiento) _direccion = 1f;
        }
        else if (tipoMovimiento == TipoMovimientooS.Rotacion)
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
