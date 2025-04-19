using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemigoSWAT : EnemigoIA
{
    public EnemyHealth   enemyHealth;
    private NavMeshAgent agent;
    public Animator animator;



    [Header("Configuración de Ataque")]
    public int dañoAlJugador = 10;
    public float tiempoEntreAtaques = 2f;

    private float tiempoParaProximoAtaque;




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
       
        agent.isStopped = true;
        agent.ResetPath();

  
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; 
            transform.rotation = Quaternion.LookRotation(direction);
        }

       
        if (Time.time >= tiempoParaProximoAtaque && distance <= distanceToAtack)
        {
            if (animator != null)
            {
                animator.SetTrigger("atacando"); 
            }

            AplicarDañoAlJugador();
            tiempoParaProximoAtaque = Time.time + tiempoEntreAtaques;
        }

        if (distance > distanceToAtack + 0.5f)
        {
            agent.isStopped = false;
            CambiarEstado(Estados.seguir);
        }
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

    public void AplicarDañoAlJugador()
    {
        if (target != null)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(dañoAlJugador);
            }
        }
    }
}
