using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TerminarExploracion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Indicaciones;

    [SerializeField] private GameObject ObjetoApagar;

    public bool Activo = false;

    private void Awake()
    {
        ObjetoApagar.SetActive(false);
        _Indicaciones.gameObject.SetActive(false);
    }
    public void Activar(bool cambio)
    {

        Activo = cambio;

        if (Activo)
        {
            ObjetoApagar.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _Indicaciones.gameObject.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        _Indicaciones.gameObject.SetActive(true);

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("CAMBIANDO DE ESCENA");
            SceneManager.LoadScene(4);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _Indicaciones.gameObject.SetActive(false);
    }

}
