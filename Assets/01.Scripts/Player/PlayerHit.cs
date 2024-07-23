using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : IHitable
{
    public bool IsDead()
    {
        return false;
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 attackedDir)
    {
    }
}
