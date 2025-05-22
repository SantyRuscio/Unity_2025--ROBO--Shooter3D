using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioPizarron : MonoBehaviour
{
    [SerializeField] private Image _miraEscondida;
    [SerializeField] private TextMeshProUGUI _Comandantes;

    [SerializeField] public AudioSource _Pizarron;
    [SerializeField] private AudioClip sonidoPizarron;
    private void Start()
    {
        _miraEscondida.gameObject.SetActive(false);
        _Comandantes.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider collision)
    {
        _Pizarron.PlayOneShot(sonidoPizarron);
        _Comandantes.gameObject.SetActive(true);

        GetComponent<BoxCollider>().enabled = false;

        StartCoroutine(AutoDestruccionTime());
    }

    IEnumerator AutoDestruccionTime()
    {
        yield return new WaitForSeconds(6.2f);
        _Comandantes.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

