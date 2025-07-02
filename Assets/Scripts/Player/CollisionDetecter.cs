using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Santiago Ruscio

public class CollisionDetecter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Item>() != null)
        {
            other.gameObject.GetComponent<Item>().ToggleShowTag(true);
        }

        if (other.gameObject.GetComponent<SystemDoor>() != null)
        {
            other.gameObject.GetComponent<SystemDoor>().ToggleShowTag(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Item>() != null)
        {
            other.gameObject.GetComponent<Item>().ToggleShowTag(false);
        }

        if (other.gameObject.GetComponent<SystemDoor>() != null)
        {
            other.gameObject.GetComponent<SystemDoor>().ToggleShowTag(false);
        }
    }
}