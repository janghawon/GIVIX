using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    private Player _player;
    private int _attakHash = Animator.StringToHash("Attack");
    private IAttackable _playerAttackCompo;

    private float _currentTime;
    private float _atkTurm;

    public PlayerAttackState(EntityState state, EntityData data, StateController controller, Entity target) : base(state, data, controller, target)
    {
        _player = target as Player;
        _playerAttackCompo = target.GetComponent<IAttackable>();
        _atkTurm = GetDataType<PlayerData>(data).attackTurm;
    }

    public override void Enter()
    {
        _reader.OnRollEvent += ChangeState;
        _reader.OnAttackEndEvent += ChangeState;
        _reader.OnMovementEvent += HandleChangeAttackRun;

        _owner.Animator.SetBool(_attakHash, true);
    }

    public override void Exit()
    {
        _reader.OnRollEvent -= ChangeState;
        _reader.OnAttackEndEvent -= ChangeState;
        _reader.OnMovementEvent -= HandleChangeAttackRun;

        _owner.Animator.SetBool(_attakHash, false);
    }

    private void HandleChangeAttackRun(EntityState state)
    {
        ChangeState(EntityState.AttackRun);
    }

    public override void Update()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _atkTurm)
        {
            _playerAttackCompo.Attack(_data.damage, _player.LookDir);
            _currentTime = 0;
        }
    }
}
