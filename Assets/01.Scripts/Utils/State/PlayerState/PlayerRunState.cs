using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRunState : State
{
    private int _runeHash = Animator.StringToHash("Run");

    public PlayerRunState(EntityState state, EntityData data, StateController controller, Entity target) : base(state, data, controller, target)
    {
    }

    public override void Enter()
    {
        _reader.OnAttackEvent += HandleChangeAttack;
        _reader.OnRollEvent += ChangeState;

        _owner.Animator.SetBool(_runeHash, true);
    }

    public override void Exit()
    {
        _reader.OnAttackEvent -= HandleChangeAttack;
        _reader.OnRollEvent -= ChangeState; 
        
        _owner.Animator.SetBool(_runeHash, false);
    }

    public override void Update()
    {
        if(_reader.MoveInput == Vector3.zero)
        {
            ChangeState(EntityState.Idle);
        }

        Vector3 move = _reader.MoveInput;
        move.z = move.y;
        move.y = 0;

        _owner.Move(move * _data.movementSpeed);
    }

    private void HandleChangeAttack(EntityState state) // Attack or Attack run
    {
        ChangeState(EntityState.AttackRun);
    }
}
