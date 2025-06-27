using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SystemDoor : MonoBehaviour, IInteractuables
{
    [Header("Referencia")]
    [SerializeField] private Transform transformPuerta;
    private Transform player;
    [SerializeField] private Item item;
    [SerializeField] private TextMeshProUGUI _Indicaciones;

    [Header("Ángulos de apertura")]
    [SerializeField] private float doorOpenAngle = -18.9f;
    [SerializeField] private float doorCloseAngle = 0f;

    [Header("Velocidad")]
    [SerializeField] private float smooth = 3f;

    [Header("Sistema de apertura")]
    [SerializeField] private bool doorOpen = false;
    private bool _canBeOpen = true; 

    [Header("AudioPuerta")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip RuidoPuerta;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(item != null)
        { 
            item.SetCanInteract(false);
        } 
    }

    public void ToggleShowTag(bool active)
    {
        _Indicaciones.gameObject.SetActive(active);
    }


    void Update()
    {
        if (doorOpen)
        {
            DoorOpen();
        }
        else
        {
            if (_canBeOpen != true)
            {
                return;
            }

            DoorClose();
        }

    }

    private void DoorOpen()
    {
        Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        transformPuerta.rotation = Quaternion.Slerp(transformPuerta.rotation, targetRotation, smooth * Time.deltaTime);
        ToggleShowTag(false);

        if(item != null)
        {
            item.SetCanInteract(true);
            _canBeOpen = false;
        }
    }

    private void DoorClose()
    {
        Quaternion targetRotation = Quaternion.Euler(0, doorCloseAngle, 0);
        transformPuerta.rotation = Quaternion.Slerp(transformPuerta.rotation, targetRotation, smooth * Time.deltaTime);
    }

    public void Interactuar(GameObject interactor)
    {
        doorOpen = !doorOpen; // Alterna entre abrir y cerrar
        AudioSource.PlayOneShot(RuidoPuerta);
    }
}