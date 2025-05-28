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


    private void OnTriggerEnter(Collider other)
    {
        ItemUpdate(other, true);
        _Indicaciones.gameObject.SetActive(true);
        Debug.Log("entro algo");
    }

    private void OnTriggerExit(Collider other)
    {
        ItemUpdate(other, false);
        Debug.Log("salio algo");
        _Indicaciones.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (CanItemBeUse() && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
            _Indicaciones.gameObject.SetActive(false);
        }
    }

    public virtual void Interactuar()
    {
       //if (CanItemBeUse())
       //{
       //    ItemInteraction();
       //}
    }
}