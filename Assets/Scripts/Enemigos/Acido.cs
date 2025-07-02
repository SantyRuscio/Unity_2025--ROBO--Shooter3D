using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Berola Lazaro

public class Acido : MonoBehaviour
{
    [SerializeField] float _damage = 10f;
    [SerializeField] float _damageTime = 2f;

    private Coroutine damageCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Alguien entro al Acido");

        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(damageable));
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }
    IEnumerator ApplyDamageOverTime(IDamageable damageable)
    {
        while (true)
        {
            damageable.TakeDamage(_damage);
            yield return new WaitForSeconds(_damageTime);
        }
    }
}
