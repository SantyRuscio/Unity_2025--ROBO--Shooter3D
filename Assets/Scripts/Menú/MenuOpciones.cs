using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Codigo por: Santiago Ruscio
public class MenuOpciones : MonoBehaviour
{
    public void VolverMenu()
    {
        SceneManager.LoadScene(0);

    }
    public void VolverReintento()
    {
        SceneManager.LoadScene(2);

    }
}
