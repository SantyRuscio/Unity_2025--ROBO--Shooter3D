using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    private bool _isFalse = false;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) && _isFalse)
        {
            RomperPlataforma();
        }
    }
    
    public void SetIsFalse(bool isfalse)
    {
        _isFalse = isfalse;
    }

    private void RomperPlataforma()
    {
        Debug.Log("¡Plataforma falsa, se rompe!");
        Destroy(gameObject);
    }
}