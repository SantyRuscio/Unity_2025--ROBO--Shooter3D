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
