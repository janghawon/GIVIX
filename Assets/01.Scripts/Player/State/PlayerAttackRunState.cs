using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRunState : PlayerBaseState
{
    private PlayerAttackState _attackState;
    private PlayerMovementState _movementState;

    public PlayerAttackRunState(StateController controller, string animationParameter) : base(controller, animationParameter)
    {
        _attackState = controller.StateControllMachine[typeof(PlayerAttackState)] as PlayerAttackState;
        _movementState = controller.StateControllMachine[typeof(PlayerMovementState)] as PlayerMovementState;
    }

    public override void EnterState()
    {
        Player.InputReader.OnRollEvent += RollHandle;
    }

    public override void UpdateState()
    {
        _attackState.UpdateState();
        _movementState.UpdateState();
    }

    public override void ExitState()
    {
        Player.InputReader.OnRollEvent -= RollHandle;
    }
}
