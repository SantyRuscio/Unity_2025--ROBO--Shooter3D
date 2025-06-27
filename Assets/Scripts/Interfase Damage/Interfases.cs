using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public void TakeDamage(float damage);
}

public interface IJump
{
    public void ChangeCanJumpState(bool NewState);
}

public interface IHeal
{
    public void Heal(int healAmount);
    bool CanRecover();
}