using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float _maxLife = 100f;
    private float _currentLife;

    void Start()
    {
        _currentLife = _maxLife; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("TOCASTE K");
            TakeDamage(50);
        }
    }

    public bool CanRecover()
    {
        if (_currentLife < _maxLife)
        { 
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(float damage)
    {
        float life = _currentLife;
        life -= damage;

        ModifyLife(life);
        CheckLife();
    }

    public void Recover()
    {
        ModifyLife(_maxLife);
    }

    private void ModifyLife(float newLife)
    {
        _currentLife = newLife;
        _currentLife = Mathf.Clamp(_currentLife, 0, _maxLife);
    }

    private void CheckLife()
    {
        if (_currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        Destroy(gameObject);
    }
}