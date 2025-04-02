using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemigoPatrulla : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 OffSet = Vector3.zero;

    private Rigidbody rb;
    //private AnimEnemigoMenu animEnemigo; 

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // animEnemigo = GetComponentInChildren<AnimEnemigoMenu>(); 
    }

    void Update()
    {
        MoverHaciaJugador();
    }

    void MoverHaciaJugador()
    {
        transform.LookAt(player);
        Vector3 finalLocation = player.position - OffSet;
        transform.position = Vector3.MoveTowards(transform.position, finalLocation, speed * Time.deltaTime);


        Vector3 dir = (finalLocation - transform.position).normalized;


        //  if (animEnemigo != null)
        {
            //       animEnemigo.ActualizarAnimacion(dir, true);
            //  }
        }
    }//}//
}
