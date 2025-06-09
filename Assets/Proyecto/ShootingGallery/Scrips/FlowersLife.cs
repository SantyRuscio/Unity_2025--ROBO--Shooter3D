using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowersLife : MonoBehaviour
{
    [SerializeField] private ParedBlocked _paredBlocked; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _paredBlocked.ParedBlockeada = false; 
            Destroy(gameObject);
            Debug.Log("Me pegó una bala");
        }
    }
}
