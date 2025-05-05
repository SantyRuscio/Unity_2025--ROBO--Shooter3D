using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnim;
    private MovAndStamina movScript;

    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>(); 
        movScript = GetComponent<MovAndStamina>(); 
    }

    void Update()
    {
        HandleAnimation();
    }

    void HandleAnimation()
    {
        Vector3 localDir = transform.InverseTransformDirection(movScript.dir);

        playerAnim.SetFloat("X", localDir.x);
        playerAnim.SetFloat("Y", localDir.z);

        if (movScript._hasWeapon)
        {
            playerAnim.SetLayerWeight(1, 1);
            playerAnim.SetBool("HoldPistol", movScript._hasPistol);
            playerAnim.SetBool("HoldRifle", movScript._hasRifle);
        }
        else
        {
            playerAnim.SetLayerWeight(1, 0);
        }


        playerAnim.SetBool("Jump", movScript._jump);
    }
}

