using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource _sonido;
    [SerializeField] private AudioClip _sonidoHover;

    public Animator animator;
    [SerializeField] private float tiempoEspera = 2.5f; 

    public void Jugar()
    {
        StartCoroutine(CambioConFade(1));
    }

    public void Controles()
    {
        StartCoroutine(CambioConFade(2));
    }

    public void Salir()
    {
        StartCoroutine(SalirConFade());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _sonido.PlayOneShot(_sonidoHover);
    }

    public void FadeOut()
    {
        if (animator != null)
            animator.Play("fade out");
    }

    private IEnumerator CambioConFade(int escenaID)
    {
        FadeOut();
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(escenaID);
    }

    private IEnumerator SalirConFade()
    {
        FadeOut();
        yield return new WaitForSeconds(tiempoEspera);
        Application.Quit();
    }
}
