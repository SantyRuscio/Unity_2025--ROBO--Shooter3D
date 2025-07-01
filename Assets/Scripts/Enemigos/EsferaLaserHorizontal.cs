using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaLaserHorizontal : LaserPadre
{
    public float velocidadMovimiento = 3f;
    public float distanciaHorizontal = 5f;

    private Vector3 _posicionInicial;
    private float _direccion = 1f;

    protected override void Start()
    {
        base.Start();
        _posicionInicial = transform.position;
    }

    protected override void EjecutarMovimiento()
    {
        transform.Translate(Vector3.right * velocidadMovimiento * _direccion * Time.deltaTime);

        if (transform.position.x > _posicionInicial.x + distanciaHorizontal)
            _direccion = -1f;
        else if (transform.position.x < _posicionInicial.x - distanciaHorizontal)
            _direccion = 1f;
    }
}

