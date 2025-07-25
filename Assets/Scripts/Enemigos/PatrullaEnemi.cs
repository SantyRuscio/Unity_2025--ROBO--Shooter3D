using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Codigo por: Beghin Ulises

[RequireComponent(typeof(NavMeshAgent))]


public class PatrullaEnemi : EnemigoIA
{
    private NavMeshAgent agent;
    public Animator animator;
    public Transform[] WayPoints;
    private int indice;
    public float distanciaWayPoints;
    private float distanciaWayPoints2;


   [Header("Configuración de Ataque")]
   [SerializeField] private float _dañoAlJugador = 10f;
   [SerializeField] private float _tiempoEntreAtaques = 2f;


    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoAtaque;
    [SerializeField] private AudioClip sonidoPesigue;
    [SerializeField] private AudioClip sonidoMuerte;

    private float tiempoParaProximoAtaque;


    private new void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        distanciaWayPoints2 = distanciaWayPoints * distanciaWayPoints;
    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (animator != null) animator.SetFloat("velocidad", 1);
        if (animator != null) animator.SetBool("atacando", false);

        agent.SetDestination(WayPoints[indice].position);
        if ((WayPoints[indice].position - transform.position).sqrMagnitude < distanciaWayPoints2)
        {
            indice = (indice+1)%WayPoints.Length;
        }
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
                StartCoroutine(GolpeEnemigo());
            }

            tiempoParaProximoAtaque = Time.time + _tiempoEntreAtaques;
        }

        if (distance > distanceToAtack + 0.5f)
        {
            agent.isStopped = false;
            _Sonido.PlayOneShot(sonidoPesigue);
            CambiarEstado(Estados.seguir);
        }
    }

    IEnumerator GolpeEnemigo()
    {
        yield return new WaitForSeconds(1.8f);
        _Sonido.PlayOneShot(sonidoAtaque);
        AplicarDañoAlJugador();
    }

    public override void EstadoMuerte()
    {
        if (EjecutarMuerte == true)
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
        _Sonido.PlayOneShot(sonidoMuerte);
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
            var damageable = target.GetComponent<IDamageable>();

            if (damageable != null && target.CompareTag("Player"))
            {
                damageable.TakeDamage(_dañoAlJugador);
            }
        }
    }
}