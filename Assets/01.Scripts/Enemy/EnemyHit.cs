using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : IHitable
{
    public bool IsDead()
    {
        return false;
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 attackedDir)
    {
        if (IsDead()) return;

        CreateHitFeedback(hitPoint, attackedDir);
    }

    private void CreateHitFeedback(Vector3 point, Vector3 dir)
    {
        var hitFx = PoolManager.Instance.Pop("HitFX") as PoolableParticle;
        hitFx.SetPositionAndRotation(point, Quaternion.LookRotation(-dir));
        hitFx.Play();
    }
}
