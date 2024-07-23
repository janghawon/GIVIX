using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct PlayerAttackCompoElementGroup
{
    public LayerMask enemyMask;
    public Transform fireTrm;
    public Transform muzzleTrm;
    public ParticleSystem cartridgeparticle;
    public float cartridgeLifeTime;
}

public partial class Player : IAttackable
{
    [Header("Player Attack")]
    public PlayerAttackCompoElementGroup _attackElementGroup;

    private float _currentTime;

    public void Attack(float damage, Quaternion dir)
    {
        if(!CanAttack()) return;

        CreateMuzzleParticle();

        Transform fireTrm = _attackElementGroup.fireTrm;

        if (Physics.Raycast(fireTrm.position, fireTrm.forward, out var hit, 100, _attackElementGroup.enemyMask))
        {
            if(hit.collider.TryGetComponent<IHitable>(out var entityHit))
            {
                Vector3 tartgetPos = hit.point;
                Vector3 atkDir = (tartgetPos - fireTrm.position).normalized;

                CreateOpoLine(fireTrm.position, tartgetPos);

                entityHit.OnDamage(_entityData.damage, tartgetPos, atkDir);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_attackElementGroup.fireTrm.position, _attackElementGroup.fireTrm.forward * 100);
    }

    public bool CanAttack()
    {
        return true;
    }

    private void CreateOpoLine(Vector3 startPos, Vector3 point)
    {
        OpoLine opo = PoolManager.Instance.Pop("OpoLine") as OpoLine;
        opo.StartOpoLine(startPos, point);
    }

    private void CreateMuzzleParticle()
    {
        var muzzle = PoolManager.Instance.Pop("MuzzleFX") as PoolableParticle;
        muzzle.transform.SetParent(_attackElementGroup.muzzleTrm);
        muzzle.transform.localPosition = Vector3.zero;
        muzzle.Play();
    }

    private void CreateCartridgeParticle()
    {
        if(!_inputReader.IsOnAttack)
        {
            _currentTime = 0;
            return;
        }

        _currentTime += Time.deltaTime;

        if(_currentTime >= _attackElementGroup.cartridgeLifeTime)
        {
            _attackElementGroup.cartridgeparticle.Play();
            
            _currentTime = 0;
        }
    }
}
