using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignadorDePlataformaFalsa : MonoBehaviour
{
    void Start()
    {
        if (transform.childCount < 2)
        {
            Debug.LogWarning("Se necesitan 2 plataformas por fila.");
            return;
        }

        // Elegir aleatoriamente cuál de las 2 es la falsa
        int falsaIndex = Random.Range(0, 2);

        for (int i = 0; i < 2; i++)
        {
            Plataforma plataforma = transform.GetChild(i).GetComponent<Plataforma>();
            if (plataforma != null)
            {
                plataforma.esFalsa = (i == falsaIndex);
            }
        }
    }
}

