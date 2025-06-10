using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource _sonido;
    [SerializeField] private AudioClip _sonidoHover;

    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Controles()
    {
        SceneManager.LoadScene(2);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _sonido.PlayOneShot(_sonidoHover);
    }
}
