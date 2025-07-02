using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Ulises beghin

public class CameraSecurityIA : EnemigoIA
{
    [Header("Ataque de C�mara")]
    [SerializeField] private GameObject electricBallPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform cabezaRotatoria;
    [SerializeField] private float cooldown = 2f;

    private bool _camaraActivada = true;

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (_camaraActivada)
        {
            SeguirTarget();
        }
        else
        {
            cabezaRotatoria.rotation = Quaternion.Euler(90f, cabezaRotatoria.rotation.eulerAngles.y, cabezaRotatoria.rotation.eulerAngles.z);
        }

    }

    public override void EstadoAtacar()
    {
        if (!vivo || target == null)
        {
            CambiarEstado(Estados.idle);
            return;
        }

        SeguirTarget();

        if (AggresiveMode)
        {
            Atacar();
        }
    }

    private void SeguirTarget()
    {
        if (cabezaRotatoria == null || target == null) return;

        Vector3 dirToTarget = (target.position - cabezaRotatoria.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dirToTarget);

        cabezaRotatoria.rotation = Quaternion.Slerp(cabezaRotatoria.rotation, lookRotation, Time.deltaTime * 2f);
    }


     public void DesactivarCamera(bool CamaraActivada)
     {
        _camaraActivada = CamaraActivada;
     }
    private void Atacar()
    {
       if(_camaraActivada)

        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

            if (angleToTarget > 90f)
            {
                CambiarEstado(Estados.idle);
                return;
            }

            if (attackTimer >= cooldown)
            {
                attackTimer = 0f;
                InstanciarBala();
            }
        }
    }

    private void InstanciarBala()
    {
        if (electricBallPrefab != null && firePoint != null)
        {
            Instantiate(electricBallPrefab, firePoint.position, firePoint.rotation);
        }
    }
}

