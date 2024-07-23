using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : State
{
    private readonly int _rollHash = Animator.StringToHash("Roll");

    private PlayerData _playerData;

    private bool _onRoll;
    private float _currentTime;

    private Vector3 _direction;

    public PlayerRollState(EntityState state, EntityData data, StateController controller, Entity target) : base(state, data, controller, target)
    {
        _playerData = GetDataType<PlayerData>(data);
    }

    public override void Enter()
    {
        _owner.Animator.SetBool(_rollHash, true);

        Transform playerTrm = _owner.transform;

        Vector3 startPos = playerTrm.position;
        Vector3 targetPos = playerTrm.position + (_owner.VisualTrm.forward * _playerData.rollDistance);

        _direction = (targetPos - startPos).normalized;

        _currentTime = 0;
        _onRoll = true;
    }

    public override void Exit()
    {
        _owner.Animator.SetBool(_rollHash, false);
    }

    public override void Update()
    {
        if(_onRoll)
        {
            _currentTime += Time.deltaTime;
            
            _owner.CharacterController.Move(_direction * 0.1f);

            if(_currentTime >= _playerData.rollDuration)
            {
                _onRoll = false;

                ChangeState(EntityState.Run);
            }
        }
    }
}
