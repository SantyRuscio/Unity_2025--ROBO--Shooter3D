using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Codigo por: Ulises Beghin
public class MenuPausa : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    public Animator animator;

    [SerializeField] private float tiempoEspera = 2.5f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;
    }

 

    public void MainMenu()

    {
        FadeOut();

        Time.timeScale = 1f;

        StartCoroutine(CambioConFade(0));
    }

    public void FadeOut()
    {
        if (animator != null)
            animator.Play("fade out");
    }


    private IEnumerator CambioConFade(int escenaID)
    {
        FadeOut();
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(escenaID);
    }
}
