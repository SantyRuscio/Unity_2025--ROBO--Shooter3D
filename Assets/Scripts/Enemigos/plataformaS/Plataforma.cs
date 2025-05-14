using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public bool esFalsa = false;
    private bool yaSeRompio = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (yaSeRompio) return;

        if (collision.gameObject.CompareTag("Player") && esFalsa)
        {
            yaSeRompio = true;
            RomperPlataforma();
        }
    }

    void RomperPlataforma()
    {
        Debug.Log("¡Plataforma falsa, se rompe!");
        Destroy(gameObject);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}

