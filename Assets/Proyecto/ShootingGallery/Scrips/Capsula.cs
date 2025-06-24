using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarEscena : MonoBehaviour
{
    private bool jugadorDentro = false;
    [SerializeField] private TextMeshProUGUI _Indicaciones;

    [Header("Fade")]

    [SerializeField] private Animator animator; 

    [SerializeField] private float tiempoEspera = 2.5f; 

    private void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CambiarConFade("Juego"));
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

    private IEnumerator CambiarConFade(string nombreEscena)
    {
        if (animator != null)
            animator.Play("fade out");

        yield return new WaitForSeconds(tiempoEspera);

        SceneManager.LoadScene(nombreEscena);
    }
}

