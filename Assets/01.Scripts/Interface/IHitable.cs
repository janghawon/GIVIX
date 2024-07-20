using UnityEngine;

public interface IHitable
{
    public void OnDamage(float damage, Vector3 attackedDir);
    public bool IsDead();
}