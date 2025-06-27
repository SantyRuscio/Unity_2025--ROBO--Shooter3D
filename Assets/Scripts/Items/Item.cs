using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Item : MonoBehaviour, IInteractuables
{
    protected abstract bool CanItemBeUse();
    //protected abstract void ItemInteraction();

    [SerializeField] protected TextMeshProUGUI _Indicaciones;

    private bool _canInteract = true;


    public void ToggleShowTag(bool active)
    {
        _Indicaciones.gameObject.SetActive(active);
    }


    public void SetCanInteract(bool InteractuarNuevo)
    {
        _canInteract = InteractuarNuevo;
    }

    public virtual void Interactuar(GameObject INteractor)
    {
        Debug.Log("LEO");
        _Indicaciones.gameObject.SetActive(false);
    }

}