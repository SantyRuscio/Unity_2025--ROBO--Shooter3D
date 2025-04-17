using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract bool CanItemBeUse();
    protected abstract void ItemInteraction();
    protected abstract void ItemUpdate(Collider other, bool HasEnter);

    private void OnTriggerEnter(Collider other)
    {
        ItemUpdate(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        ItemUpdate(other, false);
    }

    private void Update()
    {
        if (CanItemBeUse() && Input.GetKeyDown(KeyCode.E))
        {
            ItemInteraction();
        }
    }
}