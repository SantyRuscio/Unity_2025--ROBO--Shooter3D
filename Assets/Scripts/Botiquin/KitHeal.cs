using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitHeal : MonoBehaviour
{
    private PlayerLife playerLife;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLife = other.gameObject.GetComponent<PlayerLife>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLife = null;
        }
    }

    private void Update()
    {
        if (playerLife != null && Input.GetKeyDown(KeyCode.E))
        {
            if (playerLife.CanRecover())
            {
                RecoverPlayerHealth();
            }
            else
            {
                Debug.Log("El jugador ya tiene la vida completa");
            }
        }
    }

    private void RecoverPlayerHealth()
    {
        playerLife.Recover();
        Debug.Log("Jugador curado");
        Destroy(gameObject);
    }
}