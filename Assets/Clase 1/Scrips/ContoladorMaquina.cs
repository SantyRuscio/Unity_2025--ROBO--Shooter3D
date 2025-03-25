using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class ControladorMaquina : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float Rotspeed;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Move(float h, float v)
    {
        //A la posicion acutal le sumo un vector (0,0,1) local (depende de la direccion del obj) multiplicado por input
        // Y otros valores
        transform.position += transform.forward * v * speed * Time.deltaTime;

        transform.Rotate(transform.up * 360 * h * Rotspeed * Time.deltaTime);
    }

}
