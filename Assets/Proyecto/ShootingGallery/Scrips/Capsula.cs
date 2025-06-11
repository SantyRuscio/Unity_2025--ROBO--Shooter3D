using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    private bool jugadorDentro = false;
    [SerializeField] protected TextMeshProUGUI _Indicaciones;

    private void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
           // SceneManager.LoadScene("CasaPlayer1"); -- falta carga la escena en la build
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            _Indicaciones.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            _Indicaciones.gameObject.SetActive(false);
        }
    }
}

