using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemigo : EnemigoIA
{
    [Header("Configuración Sniper")]

    public float tiempoApuntado = 1f;   
    
    public float velocidadDisparo = 0.3f;    

    public GameObject balaPrefab;

    public Transform puntoDisparo;

    public float fuerzaDisparo = 30f;

    private float tiempoApuntando;

    private bool estaApuntando;

    private float tiempoEntreDisparos;  
    
    private float tiempoUltimoDisparo;     

    private void Start()
    {
        tiempoEntreDisparos = 1f / velocidadDisparo; 

        tiempoUltimoDisparo = 0f;
    }

    public override void EstadoAtacar()
    {
        if (!vivo) return;

     
        Vector3 direccion = target.position - transform.position;

        direccion.y = 0;

        transform.rotation = Quaternion.LookRotation(direccion);

   
        if (!estaApuntando)
        {
            estaApuntando = true;
            tiempoApuntando = 0f;
        }
        else
        {
            tiempoApuntando += Time.deltaTime;

       
            if (tiempoApuntando >= tiempoApuntado && Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time;
            }
        }

     
        if (distance > distanceToAtack + 0.4f)
        {
            CambiarEstado(Estados.idle);

            estaApuntando = false;
        }
    }

    private void Disparar()
    {
        
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);

        Rigidbody rb = bala.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(puntoDisparo.forward * fuerzaDisparo, ForceMode.Impulse);
        }     
    }

    public override void EstadoSeguir()
    {

        if (distance < distanceToAtack)
        {
            CambiarEstado(Estados.atacar);
        }
        else
        {
            CambiarEstado(Estados.idle);
        }
    }
}

