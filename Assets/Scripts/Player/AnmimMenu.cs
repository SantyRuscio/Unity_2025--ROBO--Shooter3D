using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesJugador : MonoBehaviour
{
    private Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }

    public void ActualizarAnimacion(Vector3 dir, bool hasPistol)
    {
        playerAnim.SetFloat("X", dir.x);
        playerAnim.SetFloat("Y", dir.z);
        playerAnim.SetBool("HoldPistol", hasPistol);

        if (hasPistol)
        {
            playerAnim.SetLayerWeight(1, 1);
        }
    }
}
