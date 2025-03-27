using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisiones : MonoBehaviour
{
    public EstadoMovimiento estadoMovimiento;
    public GameObject nearItem;
    public GameObject itemPrefab;
    public Transform itemSlot;
    void Start()
    {
        {
            estadoMovimiento = GetComponent<EstadoMovimiento>();
            estadoMovimiento = GameObject.FindWithTag("Player").GetComponent<EstadoMovimiento>();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        ItemLogic();
    }

    public void ItemLogic()
    {

        if (nearItem != null && Input.GetKeyDown(KeyCode.E))
        {
            GameObject instantiatedItem = Instantiate(itemPrefab, itemSlot.position, itemSlot.rotation);
            Destroy(nearItem.gameObject);
            instantiatedItem.transform.parent = itemSlot;

            // REFERENCIA AL OTRO SCRIPT
            if (estadoMovimiento != null)
            {
                estadoMovimiento._hasPistol = true;
            }

            nearItem = null;
        }
    }
    void OnTriggerEnter(Collider other)
     {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("Hay Un Item Cera");
            nearItem = other.gameObject;
        }
     }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("Ya no Hay Item");
            nearItem = null ;
        }
    }

}
