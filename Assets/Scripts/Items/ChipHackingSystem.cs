using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChipHackingSystem : Item
{
    private MovAndStamina _jugador;

    [SerializeField] private float _timerSound = 0.99f;

    [Header("Audio")]
    [SerializeField] public AudioSource _Sonido;
    [SerializeField] private AudioClip sonidoHackear;

    protected override bool CanItemBeUse()
    {
        return _jugador != null;
    }

    public override void Interactuar()
    {
        PlayerAnimator playerAnimator = _jugador.GetComponent<PlayerAnimator>();
        if (playerAnimator != null)
        {
            playerAnimator.TriggerSpecialAnimation();
        }

        CameraSecurityIA camaraSeguridad = FindObjectOfType<CameraSecurityIA>();
        if (camaraSeguridad != null)
        {
            camaraSeguridad.DesactivarCamera(false);
            Debug.Log("ChipHackingSystem activado");
        }

        GetComponent<Collider>().enabled = false;

        StartCoroutine(ReproducirHacking());
    }

    private IEnumerator ReproducirHacking()
    {
        _Sonido.PlayOneShot(sonidoHackear);
        yield return new WaitForSeconds(_timerSound); // espera que el audio termine
        gameObject.SetActive(false); // ahora sí se puede apagar
        Destroy(gameObject);
    }

    protected override void ItemUpdate(Collider other, bool HasEnter)
    {
        if (other.CompareTag("Player"))
        {
            if (HasEnter)
            {
                _jugador = other.gameObject.GetComponent<MovAndStamina>();
                Debug.Log("Jugador cerca del ChipHackingSystem");
            }
            else
            {
                _jugador = null;
            }
        }
    }
}