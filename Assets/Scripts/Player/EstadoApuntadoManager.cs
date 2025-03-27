using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EstadoApuntadoManager : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;

    [SerializeField] Transform camFollowPos;

    private EstadoMovimiento estadoMovimiento;

    void Start()
    {
        estadoMovimiento = FindObjectOfType<EstadoMovimiento>(); // Encuentra el script del jugador
    }

    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        if (estadoMovimiento != null && estadoMovimiento._hasPistol)
        {
            // Ya no hay limitación de ángulos, por lo que esta parte se elimina
        }
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}

