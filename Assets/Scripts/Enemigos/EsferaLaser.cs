using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaLaser : LaserPadre
{
    public float velocidadRotacion = 5f;

    protected override void EjecutarMovimiento()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}
