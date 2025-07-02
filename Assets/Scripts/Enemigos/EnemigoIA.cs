
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Berola, Ruscio, Beghin
public abstract class EnemigoIA : MonoBehaviour
{
    public Estados estado;

    public float distanceToFollow;
    public float distanceToAtack;
    public float distanceToEscape;

    public bool autoSelectTarget = false;
    public Transform target;
    public float distance;

    public bool vivo = true;
    protected bool EjecutarMuerte = true;

    protected bool AggresiveMode = false;

    public virtual void Awake()
    {
        if (autoSelectTarget)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        StartCoroutine(CalcularDistancia());
    }

    private void LateUpdate()
    {
        CheckEstado();
    }

    public bool Agresive
    {
        get
        {
            return AggresiveMode;
        }
        set
        {
            AggresiveMode = value;
        }
    }
    private void CheckEstado()
    {
        switch (estado)
        {
            case Estados.idle:
                EstadoIdle();
                break;
            case Estados.seguir:
                EstadoSeguir();
                break;
            case Estados.atacar:
                EstadoAtacar();
                break;
            case Estados.muerto:
                EstadoMuerte();
                break;
        }
    }

    public void CambiarEstado(Estados e)
    {
        switch (e)
        {
            case Estados.muerto:
                vivo = false;
                break;
        }
        estado = e;
    }

    public virtual void EstadoIdle()
    {
        if (!vivo)
        {
            CambiarEstado(Estados.muerto);
            return;
        }

        if (distance < distanceToFollow && AggresiveMode)
        {
            CambiarEstado(Estados.seguir);
        }
    }

    public virtual void EstadoSeguir()
    {
        if (!vivo)
        {
            CambiarEstado(Estados.muerto);
            return;
        }

        if (distance < distanceToAtack)
        {
            CambiarEstado(Estados.atacar);
        }
        else if (distance > distanceToEscape)
        {
            CambiarEstado(Estados.idle);
        }
    }

    public virtual void EstadoAtacar()
    {
        if (!vivo)
        {
            CambiarEstado(Estados.muerto);
            return;
        }

        if (distance > distanceToAtack + 0.4f)
        {
            CambiarEstado(Estados.seguir);
        }
    }

    public virtual void EstadoMuerte() { }

    // 🔧 Este método ahora es virtual y protegido para que las clases hijas puedan sobrescribirlo
    protected virtual IEnumerator CalcularDistancia()
    {
        while (vivo)
        {
            if (target != null)
            {
                distance = Vector3.Distance(transform.position, target.position);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToAtack);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToFollow);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToEscape);
    }
}

public enum Estados
{
    idle = 0,
    seguir = 1,
    atacar = 2,
    muerto = 3,
}

