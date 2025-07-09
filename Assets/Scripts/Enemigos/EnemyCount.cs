using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoContador; 

    private int enemigosEnEscena;

    void Update()
    {
        
        enemigosEnEscena = GameObject.FindGameObjectsWithTag("Enemy").Length;

        textoContador.text =" " + enemigosEnEscena.ToString();
    }
}
