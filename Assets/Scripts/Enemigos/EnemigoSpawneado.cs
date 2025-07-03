using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoSpawneado : MonoBehaviour
{
    private EnemySpawner spawner;
    private EnemigoIA enemigoIA;

    private void Start()
    {
        enemigoIA = GetComponent<EnemigoIA>();
    }

    public void SetSpawner(EnemySpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    public void SetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && enemigoIA != null)
        {
            enemigoIA.SetTarget(player.transform);
        }
    }

    private void Update()
    {
        if (enemigoIA != null && enemigoIA.estado == Estados.muerto)
        {
            if (spawner != null)
            {
                spawner.NotifyEnemyDeath();
                spawner = null;
            }
        }
    }
}
