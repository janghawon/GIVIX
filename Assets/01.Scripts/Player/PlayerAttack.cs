using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public struct PlayerAttackCompoElementGroup
{
    public LayerMask enemyMask;
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
        var muzzle = PoolManager.Instance.Pop("MuzzleFX") as PoolableParticle;
        muzzle.transform.SetParent(_attackElementGroup.muzzleTrm);
        muzzle.transform.localPosition = Vector3.zero;   
        muzzle.Play();
    }

    public bool CanAttack()
    {
        return true;
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
