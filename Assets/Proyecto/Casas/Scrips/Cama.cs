using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cama : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tocaLaLetra;
    private AudioGenerico AudioGenerico;
    [SerializeField] private int Scene;

    private void Start()
    {
        _tocaLaLetra.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("choque con algo");
        _tocaLaLetra.gameObject.SetActive(true);

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("CAMBIANDO DE ESCENA");
            NextScene();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _tocaLaLetra.gameObject.SetActive(false);
    }
    private void NextScene()
    {
        Debug.Log("CARGANDO ESCENA");
        SceneManager.LoadScene(Scene); //fALTA poner el nombre que va realmente
    }
}

