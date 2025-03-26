using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionMouse : MonoBehaviour
{
    [SerializeField]
    private Camera Cam;

    [SerializeField]
    float Sens;

    [SerializeField] 
    private Plane groundPlane;

    [SerializeField] 
    private float rayLenght;
    // Start is called before the first frame update
    void Start()
    {
        //asigna la camara principal a la variable cam para tomarla de referencia a ka posicion del pountero
        Cam= Camera.main;
        //crea un plano para detectar la posicion del mouse en funcion del rayo que lo tocara
        groundPlane = new Plane(Vector3.up, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //crea un rayo desde la camara hasta el puntero del mouse
        Ray camRay = Cam.ScreenPointToRay(Input.mousePosition);

        //si el rayo intersepta el,plano
        if (groundPlane.Raycast(camRay, out rayLenght))
        {
            // obtienen la posicion del puntero del mouse en el plano
            Vector3 pointToLook= camRay.GetPoint(rayLenght);

            // hace que el objetivo mire hacia el puntero del mouse
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z ) * Sens);  
        }
    }
}
