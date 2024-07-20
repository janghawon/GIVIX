using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable 
{
    public bool CanAttack();
    public void Attack(float damage, Quaternion dir);
}
