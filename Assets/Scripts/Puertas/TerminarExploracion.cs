using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminarExploracion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Indicaciones;

    [SerializeField] private GameObject ObjetoApagar;

    public bool Activo = false;

    private void Awake()
    {
        ObjetoApagar.SetActive(false);

        _Indicaciones = false;
    }

    void Update()
    {
        
    }

    private void ActivarObjeto()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            _Indicaciones.gameObject.SetActive(true);
        }
    }
}
