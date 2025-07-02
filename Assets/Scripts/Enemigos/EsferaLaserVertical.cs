using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Beghin Ulises

public class EsferaLaserVertical : LaserPadre
{
    public float velocidadMovimiento = 3f;
    public float distanciaVertical = 5f;

    private Vector3 _posicionInicial;
    private float _direccion = 1f;

    protected override void Start()
    {
        base.Start();
        _posicionInicial = transform.position;
    }

    protected override void EjecutarMovimiento()
    {
        transform.Translate(Vector3.up * velocidadMovimiento * _direccion * Time.deltaTime);

        if (transform.position.y > _posicionInicial.y + distanciaVertical)
            _direccion = -1f;
        else if (transform.position.y < _posicionInicial.y - distanciaVertical)
            _direccion = 1f;
    }
}

