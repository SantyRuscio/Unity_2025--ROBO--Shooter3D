using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Codigo por: Santiago Ruscio
public class CondicionVictoria : MonoBehaviour
{
    [SerializeField] private string escenaVictoria = "Victoria";
    [SerializeField] private string tagEnemigo = "Enemy";

    private void Update()
    {
        
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigo);

        if (enemigos.Length == 0)
        {
            SceneManager.LoadScene(escenaVictoria);
        }
    }

    
    private void OnDestroy()
    {
        if (CompareTag("Enemy"))
        {
            Debug.Log($"{gameObject.name} con tag 'Enemy' ha sido destruido.");
        }
    }
}

