using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo por: Berola Lazaro

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
        Vector3 localDir = transform.InverseTransformDirection(_movAndStramina.GetMovementDirection);

        _playerAnim.SetFloat("X", localDir.x);
        _playerAnim.SetFloat("Y", localDir.z);

        if (_weaponPlayerScript.HasWeapon)
        {
            _playerAnim.SetLayerWeight(1, 1);

            _playerAnim.SetBool("HoldPistol", _weaponPlayerScript.CurrentWeaponType == WeaponType.Pistol);
            _playerAnim.SetBool("HoldRifle",  _weaponPlayerScript.CurrentWeaponType == WeaponType.Rifle);
        }
        else
        {
            _playerAnim.SetLayerWeight(1, 0);
        }

        _playerAnim.SetBool("Jump", _movAndStramina.GetIsJumping());
    }
    public void TriggerSpecialAnimation()
    {
        _playerAnim.SetTrigger("SpecialAction"); 
    }
    public void TriggerSpecialElectricBall()
    {
        _playerAnim.SetTrigger("GolpeElectrico");
    }
}