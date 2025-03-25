using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCamera : MonoBehaviour
{
    //El objetivo de la camara (jugador)
    [SerializeField] Transform target;

    //Velocidad a la que se ajusta la camara cuando usamos suavizado
    [SerializeField] float lerpSpeed;
    [SerializeField] float slerpSpeed;

    //Distancia hacia la posicion exacta del objetivo
    [SerializeField] float verticalOffset;
    [SerializeField] float horizontalOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        #region Sin Suavizado
        /*
        //La camara automaticamente mira en la misma direccion que el objetivo
        transform.forward = target.forward;

        //La posicion de la camara va a ser la misma del objetivo, con offsets para que no este incrustada
        //Usamos Vector3.up para que siempre este un poco mas arriba
        //Y -transform.forward porque "atras" va a depender de donde mira la camara. Va atras para que podamos ver al objetivo
        transform.position = target.position + Vector3.up * verticalOffset - transform.forward * horizontalOffset;
        */
        #endregion

        #region Con Suavizado (Lerp y SLerp)
        //Lerp viene de Linear Interpolation, que es buscar un valor intermedio entre otros dos valores
        //Tenemos 3 variedades de Lerp en Unity
        //Mathf.Lerp para dos numeros
        //Vector3.Lerp para dos posiciones
        //Vector3.Slerp para dos angulos
        //En todos los casos necesitamos 3 valores: los dos datos que queremos interpolar, y un factor de interpolacion
        //Ese ultimo tiene que ser un numero entre 0 y 1. Si es 0 nos quedamos con el primer dato, y si es 1 con el segundo
        //0.5f nos da exactamente el medio entre los dos, y asi sucesivamente con otros numeros intermedios

        //Con Slerp podemos hacer que la camara vaya girando lentamente para alinearse con el objetivo
        //De esta manera cuando el auto gire, la camara tambien va a ir girando
        //Siendo que Time.deltaTime es un valor muy chico, el primer dato de Slerp y Lerp tiene que ser el "actual" de la camara
        //Multiplicamos a Time.deltaTime por la velocidad, asi cuando subimos la velocidad se acerca mas al del objetivo
        transform.forward = Vector3.Slerp(transform.forward, target.forward, slerpSpeed * Time.deltaTime);

        //Lerp es lo mismo pero para la posicion, vamos a interpolar entre donde estan la camara y el objetivo

        //Separamos este vector para que entre en la misma linea y se visualice bien, pero no es necesario
        Vector3 nextPos = target.position + Vector3.up * verticalOffset - transform.forward * horizontalOffset;
        
        transform.position = Vector3.Lerp(transform.position, nextPos, lerpSpeed * Time.deltaTime);
        #endregion
    }
}
