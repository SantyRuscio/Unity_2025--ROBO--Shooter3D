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
    public Animator animator;

    private float tiempoApuntando;
    private bool estaApuntando;
    private float tiempoEntreDisparos;
    private float tiempoUltimoDisparo;

    private void Start()
    {
        tiempoEntreDisparos = 1f / velocidadDisparo;
        tiempoUltimoDisparo = 0f;
    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();
        estaApuntando = false;
        tiempoApuntando = 0f;
        // No se necesita cambiar animaciones, idle es por defecto
    }

    public override void EstadoSeguir()
    {
        base.EstadoSeguir();

        if (distance < distanceToAtack)
        {
            CambiarEstado(Estados.atacar);
        }
        else
        {   
            CambiarEstado(Estados.idle);
        }
    }

    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        if (!vivo) return;

        // Girar hacia el objetivo
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
                tiempoApuntando = 0f;
            }
        }

        if (distance > distanceToAtack + 0.4f)
        {
            CambiarEstado(Estados.idle);
        }
    }

    private void Disparar()
    {
        if (animator != null)
            animator.SetTrigger("isShooting");

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        bala.transform.LookAt(target.position);
        Rigidbody rb = bala.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(puntoDisparo.forward * fuerzaDisparo, ForceMode.Impulse);
        }
    }

    public override void EstadoMuerte()
    {
        if (EjecutarMuerte)
        {
            EjecutarMuerte = false;
            base.EstadoMuerte();

            if (animator != null)
            {
                animator.SetTrigger("Death");
                StartCoroutine(DeathAnim());
            }
        }
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void matar()
    {
        CambiarEstado(Estados.muerto);
    }
}

