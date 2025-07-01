using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    Rigidbody bulletRb;

    [SerializeField] float _bulletPower = 50f;
    [SerializeField] float _lifeTime = 10f;
    [SerializeField] float _damage = 10f;
    [SerializeField] float _paralyzeTime = 1.5f;

    private MovAndStamina _jugador;

    [SerializeField]  private float time = 0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * _bulletPower, ForceMode.Impulse);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _jugador = player.GetComponent<MovAndStamina>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bala impactó contra: " + other.gameObject.name);

        PlayerAnimator playerAnimator = _jugador.GetComponent<PlayerAnimator>();

        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);

            var playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth || playerAnimator  != null)
            {
                playerHealth.Paralyze(_paralyzeTime);

                playerAnimator.TriggerSpecialElectricBall();
            }
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > _lifeTime)
        {
            Destroy(gameObject);
        }
    }

}