using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignadorDePlataformaFalsa : MonoBehaviour
{
    [SerializeField] private Plataforma[] _childPlatforms;

    [SerializeField] private int _numberOfPlatfoms = 2;


    private void Start()    
    {

        if (_childPlatforms.Length < _numberOfPlatfoms)
        {
            Debug.LogWarning("Se necesitan " + _numberOfPlatfoms + " plataformas por fila.");
            return;
        }

        // Elegir aleatoriamente cu�l de las 2 es la falsa
        int falsaIndex = Random.Range(0, _childPlatforms.Length-1);

        for (int i = 0; i < _childPlatforms.Length - 1; i++)
        {
            _childPlatforms[i].SetIsFalse(i == falsaIndex);
        }
    }
}