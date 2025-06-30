using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cuadro : Item
{
    [SerializeField] private RawImage _cuadroImagen;

    [Header("Audio")]
    [SerializeField] private AudioSource _Sonido;
    [SerializeField] private AudioClip _sonidoCuadro;

    protected override bool CanItemBeUse()
    {
        return true;
    }

    public override void Interactuar(GameObject Interactor)
    {
        if (CanItemBeUse())
        {
            if (_cuadroImagen != null)
            {
                bool isActive = _cuadroImagen.gameObject.activeSelf;
                _cuadroImagen.gameObject.SetActive(!isActive);

                if (_Sonido != null && _sonidoCuadro != null)
                {
                    _Sonido.PlayOneShot(_sonidoCuadro);
                }
            }
            base.Interactuar(Interactor);
        }
    }
}
