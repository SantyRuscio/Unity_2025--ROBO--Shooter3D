using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
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

    public bool AggresiveMode = false;

    public void Awake()
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

    private void CheckEstado()
    {
        switch (estado)
        {
            case Estados.idle:
                EstadoIdle();
                break;
            case Estados.seguir:
                //transform.LookAt(target, Vector3.up);
                EstadoSeguir();
                break;
            case Estados.atacar:
                EstadoAtacar();
                break;
            case Estados.muerto:
                EstadoMuerte();
                break;
            default:
                break;

        }
    }

    public void CambiarEstado(Estados e)
    {
        switch (e)
        {
            case Estados.idle:
                break;
            case Estados.seguir:
                break;
            case Estados.atacar:
                break;
            case Estados.muerto:
                vivo = false;   
                break;
            default:
                break;
        }
        estado = e;
    }
    public virtual void EstadoIdle()
    {
        if (vivo == false)
        {
            CambiarEstado(Estados.muerto);
            return;
        }

        if (distance < distanceToFollow && AggresiveMode == true )
        {
            CambiarEstado(Estados.seguir);
        }
    }

    public virtual void EstadoSeguir()
    {
        if (vivo == false)
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
        if (vivo == false)
        {
            CambiarEstado(Estados.muerto);
            return;
        }

        if (distance > distanceToAtack + 0.4f)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    public virtual void EstadoMuerte()
    {

    }

    IEnumerator CalcularDistancia()
    {
        while (vivo)
        {
            if( target!=null)
            {
                distance = Vector3.Distance(transform.position, target.position);
                yield return new WaitForSeconds(0.3f);
            }
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
    idle   = 0,
    seguir = 1,
    atacar = 2,
    muerto = 3,
}
