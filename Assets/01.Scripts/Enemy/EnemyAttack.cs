using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : IAttackable
{
    public void Attack(float damage, Quaternion dir)
    {
    }

    public bool CanAttack()
    {
        return false;
    }
}
