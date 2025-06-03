using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorDiamond : MonoBehaviour
{
    [Header("Referencia")]
    [SerializeField] private Transform doorTransform;
    private Collider _doorCollider;

    [Header("Apertura")]
    [SerializeField] private Vector3 openOffset = new Vector3(-8f, 0f, 0f); // cuánto se desliza la puerta
    [SerializeField] private float openSpeed = 2f;
    private Vector3 _closedPosition;
    private Vector3 _openPosition;

    [Header("Checkers")]
    private bool _isOpen = false;
    private bool _isOpening = false;
    private bool _playerInRange = false;
    private bool _hasBeenOpened = false;
    [SerializeField] private bool CardPick = false;

    [Header("Audios")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip RuidoPuerta;

    [SerializeField] protected TextMeshProUGUI _IndicacionesCerrada;
    [SerializeField] protected TextMeshProUGUI _IndicacionesAbierta;

    private void Start()
    {
        _closedPosition = doorTransform.position;
        _openPosition = _closedPosition + openOffset;
        _doorCollider = doorTransform.GetComponent<Collider>();
    }

    private void Update()
    {
        Helps();
        OpenDoorInput();
        DoorMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
    public void CardPicked(bool card)
    {
        CardPick = card;
    }
    
    private void Helps()
    {
        if (_playerInRange)
        {
            InRange();
        }
        else
        {
            _IndicacionesAbierta.gameObject.SetActive(false);
            _IndicacionesCerrada.gameObject.SetActive(false);
        }
    }

    private void OpenDoorInput()
    {

        if (_playerInRange && !_hasBeenOpened && Input.GetKeyDown(KeyCode.E) && CardPick)
        {
            _isOpening = true;
            _hasBeenOpened = true;
            StartCoroutine(Audio());
        }
    }

    private void DoorMovement()
    {
        if (_isOpening)
        {
            doorTransform.position = Vector3.Lerp(doorTransform.position, _openPosition, Time.deltaTime * openSpeed);

            if (_doorCollider != null && Vector3.Distance(doorTransform.position, _openPosition) < 0.1f)
            {
                _doorCollider.enabled = false;
                _isOpen = true;
                _isOpening = false;
            }
        }
    }
    private void InRange()
    {
        if (_playerInRange && CardPick)
        {
            InRangeOpen();
        }
        else if (_playerInRange && !CardPick)
        {
            _IndicacionesCerrada.gameObject.SetActive(true);
        }
    }
    private void InRangeOpen()
    {
        if (_isOpen)
        {
            _IndicacionesAbierta.gameObject.SetActive(false);
        }
        else if (_isOpening)
        {
            _IndicacionesAbierta.gameObject.SetActive(false);
        }
        else
        {
            _IndicacionesAbierta.gameObject.SetActive(true);
        }
    }
    private IEnumerator Audio()
    {
        AudioSource.PlayOneShot(RuidoPuerta);
        yield return new WaitForSeconds(1.5f);
    }
}
