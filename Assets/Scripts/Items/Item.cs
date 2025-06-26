using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Item : MonoBehaviour, IInteractuables
{
    protected abstract bool CanItemBeUse();
    //protected abstract void ItemInteraction();
    protected abstract void ItemUpdate(Collider other, bool HasEnter);

    [SerializeField] protected TextMeshProUGUI _Indicaciones;

    private bool _canInteract = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == false) return;

        ItemUpdate(other, true);
       _Indicaciones.gameObject.SetActive(true);
        Debug.Log(gameObject.name + "  entro algo   " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        ItemUpdate(other, false);
        Debug.Log("salio algo");
        _Indicaciones.gameObject.SetActive(false);
    }

    public void SetCanInteract(bool InteractuarNuevo)
    {
        _canInteract = InteractuarNuevo;
    }

    public virtual void Interactuar()
    {
        Debug.Log("LEO");
        _Indicaciones.gameObject.SetActive(false);
    }
}