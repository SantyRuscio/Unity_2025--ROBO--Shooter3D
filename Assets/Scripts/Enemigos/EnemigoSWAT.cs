using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemigoSWAT : EnemigoIA
{
    public EnemyHealth   enemyHealth;
    private NavMeshAgent agent;
    public Animator  animator;

    // Start is called before the first frame update

    private void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (animator != null) animator.SetFloat("velocidad", 0);
        if (animator != null) animator.SetBool("atacando", false);
        agent.SetDestination(transform.position);
    }
    public override void EstadoSeguir()
    {
        base.EstadoSeguir();
        if (animator != null) animator.SetFloat("velocidad", 1);
        if (animator != null) animator.SetBool("atacando", false);
        agent.SetDestination(target.position);
    }
    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        if (animator != null) animator.SetFloat("velocidad", 0);
        if (animator != null) animator.SetBool("atacando", true);
        agent.SetDestination(transform.position);
        transform.LookAt(target, Vector3.up);
    }
    public override void EstadoMuerte()
    {
        if(EjecutarMuerte == true)
        {
            EjecutarMuerte = false;
            base.EstadoMuerte();

            if (animator != null)
            {
                Die();
            }
            agent.enabled = false;
        }
    }


    public void Die()
    {
        Debug.Log("Enemigo muerto");
        animator.SetTrigger("Death");
        StartCoroutine(DeathAnim());

    }
    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void matar()
    {
        CambiarEstado(Estados.muerto);
    }

}
