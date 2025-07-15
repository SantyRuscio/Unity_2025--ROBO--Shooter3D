using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Codigo por: Beghin Ulises

public class PlayerHealth : MonoBehaviour, IDamageable, IHeal, IParalyze
{
    [Header("Configuración de Vida")]

    public float maxHealth = 100;
    public float currentHealth;
    public bool isDead = false;
    private float DurationParalyzed = 1.5f;

    [Header("AudiosEspeciales")]
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioElectrico;

    [Header("AudioCurarse")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoBotiquin;

    [Header("UI")]
    public Image vidaImagen; 

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10); // daño de prueba

            Debug.Log("Vida actual: " + currentHealth);
        }

        // curacion de prueba
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);

            Debug.Log("Vida actual: " + currentHealth);
        }
    } //Daños y curaciones de prueba

    void IParalyze.TakeDamageParalyzed(float damage)
    {
        var duration = DurationParalyzed;

        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        if (_hurtClip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(_hurtClip);
            Paralyze(duration);

        }

        UpdateHealthUI();

        //chequear si murio
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Paralyze(float duration)
    {
        _audioSource.PlayOneShot(_audioElectrico);

        var playerAnimator = GetComponent<PlayerAnimator>(); 

        if (playerAnimator != null)
        {
            playerAnimator.TriggerSpecialElectricBall();
        }

        StartCoroutine(ParalyzeCoroutine(duration));
    }


    private IEnumerator ParalyzeCoroutine(float duration)
    {
        var moveScript = GetComponent<MovAndStamina>();
        if (moveScript != null)
        {
            moveScript.SetMovementEnabled = false ;
            yield return new WaitForSeconds(duration);
            moveScript.SetMovementEnabled = true;
        }
    }

    public bool CanRecover()
    {
        return currentHealth < maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if (isDead) return; 

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        if (_hurtClip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(_hurtClip);
        }

        UpdateHealthUI();

        //chequear si murio
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //curarse
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        currentHealth = Mathf.Min(currentHealth, maxHealth);

        _Sonido.PlayOneShot(sonidoBotiquin);

        UpdateHealthUI();
    }

    public bool Heal() //del botiquin
    {

        return currentHealth < maxHealth;
    }

    public void Recover()
    {
        if (isDead) return;

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    //metodo para morir
    void Die()
    {
        isDead = true;
        
        var moveandStamina = GetComponent<MovAndStamina>(); 

        moveandStamina.ChangeCameraEnableState(false);
        moveandStamina.enabled = false;

        SceneManager.LoadScene(4);
    }

    // reiniciar el nivel 
    void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateHealthUI()
    {
        if (vidaImagen != null)
            vidaImagen.fillAmount = (float)currentHealth / maxHealth; 

    }
}