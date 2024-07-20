using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    private int _idleHash = Animator.StringToHash("Idle");

    public PlayerIdleState(EntityState state, EntityData data, StateController controller, Entity target) : base(state, data, controller, target) { }

    public override void Enter()
    {
        _reader.OnMovementEvent += ChangeState;
        _reader.OnAttackEvent += ChangeState;
        _reader.OnRollEvent += ChangeState;

        _owner.Animator.SetBool(_idleHash, true);
    }

    public override void Exit()
    {
        _reader.OnMovementEvent -= ChangeState;
        _reader.OnAttackEvent -= ChangeState;
        _reader.OnRollEvent -= ChangeState;

        _owner.Animator.SetBool(_idleHash, false);
    }

    public override void Update()
    {
    }
}
