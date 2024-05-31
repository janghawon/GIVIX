using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private Vector3 _attackDir;
    private float _triggerCalledTime;
    private readonly LayerMask _groundMask;
    private Coroutine _runningRoutine;

    public bool IsAttacking { get; set; }
    private float _currentTime;
    private float _maxTime;

    public PlayerAttackState(StateController controller, string animationParameter) : base(controller, animationParameter)
    {
        _groundMask = LayerMask.GetMask("Ground");
    }

    public override void EnterState()
    {
        base.EnterState();
        
        Player.InputReader.OnRollEvent += RollHandle;
        Player.InputReader.OnAttackEndEvent += AttackEndHandle;
        Player.InputReader.OnAttackRunEvent += AttackRunHandle;

        if (_runningRoutine is not null)
        {
            Player.StopCoroutine(_runningRoutine);
        }

        Attack();
        IsAttacking = true;
    }

    public override void UpdateState()
    {
        if(IsAttacking)
        {
            _currentTime += Time.deltaTime;
            if(_currentTime >= Player.PlayerData.attackDelayTime)
            {
                Attack();
                _currentTime = 0;
            }
        }

    }

    public override void ExitState()
    {
        IsAttacking = false;
        base.ExitState();
        Player.InputReader.OnRollEvent -= RollHandle;
        Player.InputReader.OnAttackEndEvent -= AttackEndHandle;
        Player.InputReader.OnAttackRunEvent -= AttackRunHandle;
    }

    private void Attack()
    {
        var damageableObjects = Player.GetDamageableObjects(out var points);
        for (var i = 0; i < damageableObjects.Count; i++)
        {
            if (damageableObjects[i].IsDead())
            {
                continue;
            }
            
            AttackFeedback(points[i]);
            damageableObjects[i].OnDamage(Player.PlayerData.damage, _attackDir);
        }
    }

    private void AttackFeedback(Vector3 point)
    {
        var hitParticle = PoolManager.Instance.Pop("HitParticle") as PoolableParticle;
        point += Random.insideUnitSphere * 0.2f;
        hitParticle.SetPositionAndRotation(point, Quaternion.identity);
        hitParticle.Play();
    }
}