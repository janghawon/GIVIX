using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRunState : State
{
    private int _attakRunHash = Animator.StringToHash("AttackRun");
    private IAttackable _playerAttackCompo;

    private float _currentTime;
    private float _atkTurm;

    private PlayerData _playerData;
    private Player _player;

    public PlayerAttackRunState(EntityState state, EntityData data, StateController controller, Entity target) : base(state, data, controller, target)
    {
        _player = _owner as Player;
        _playerAttackCompo = target.GetComponent<IAttackable>();
        _playerData = GetDataType<PlayerData>(data);
    }

    public override void Enter()
    {
        _reader.OnRollEvent += ChangeState;
        _reader.OnAttackEndEvent += ChangeState;

        _owner.Animator.SetBool(_attakRunHash, true);

        if (!_player.CursorController.IsCursorActive)
            _player.CursorController.VisableCursor();
    }

    public override void Exit()
    {
        _reader.OnRollEvent -= ChangeState;
        _reader.OnAttackEndEvent -= ChangeState;

        _owner.Animator.SetBool(_attakRunHash, false);
        _player.CursorController.UnVisibleCursor();
    }

    public override void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _atkTurm)
        {
            _playerAttackCompo.Attack(_data.damage, _player.LookDir);
            _currentTime = 0;
        }

        if (_reader.MoveInput == Vector3.zero)
        {
            ChangeState(EntityState.Attack);
        }

        Vector3 move = _reader.MoveInput;
        move.z = move.y;
        move.y = 0;

        _owner.Move(move * _playerData.attackMovementSpeed);
    }
}
