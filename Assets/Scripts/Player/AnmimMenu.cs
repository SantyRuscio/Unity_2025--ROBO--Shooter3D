using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnim;
    private WeaponPlayer _weaponPlayerScript;
    private MovAndStamina _movAndStramina;

    void Start()
    {
        _playerAnim = GetComponentInChildren<Animator>(); 
        _weaponPlayerScript = GetComponent<WeaponPlayer>();
        _movAndStramina = GetComponent<MovAndStamina>(); 
    }

    private void Update()
    {
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        Vector3 localDir = transform.InverseTransformDirection(_movAndStramina.GetMovementDirection());

        _playerAnim.SetFloat("X", localDir.x);
        _playerAnim.SetFloat("Y", localDir.z);

        if (_weaponPlayerScript.GetHasWeapon())
        {
            _playerAnim.SetLayerWeight(1, 1);
            _playerAnim.SetBool("HoldPistol", _weaponPlayerScript.GetHasPistol());
            _playerAnim.SetBool("HoldRifle", _weaponPlayerScript.GetHasRifle());
        }
        else
        {
            _playerAnim.SetLayerWeight(1, 0);
        }

        _playerAnim.SetBool("Jump", _movAndStramina.GetIsJumping());
    }
}