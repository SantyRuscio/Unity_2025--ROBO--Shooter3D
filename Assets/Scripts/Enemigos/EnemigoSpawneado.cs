using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSpawneado : MonoBehaviour
{
    private EnemySpawner spawner;
    private EnemigoIA enemigoIA;

    private void Start()
    {
        enemigoIA = GetComponent<EnemigoIA>();

        if (enemigoIA != null)
        {
            enemigoIA.Agresive = true;
        }
    }

    public void SetSpawner(EnemySpawner spawnerRef)
    {
        spawner = spawnerRef;
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
