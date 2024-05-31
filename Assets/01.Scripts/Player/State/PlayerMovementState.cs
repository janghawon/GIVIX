using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    private readonly int _movementHash;
    
    public PlayerMovementState(StateController controller, string animationParameter) : base(controller, animationParameter)
    {
        _movementHash = Animator.StringToHash(animationParameter);
    }

    public override void EnterState()
    {
        base.EnterState();
        Player.InputReader.OnAttackEvent += AttackHandle;
        Player.InputReader.OnAttackEndEvent += AttackEndHandle;
        Player.InputReader.OnRollEvent += RollHandle;
    }

    public override void UpdateState()
    {
        var inputDir = Player.InputReader.movementInput;
        MovementDirAnimatorParameterSet(inputDir);

        if (_animationTriggerCalled)
        {
            CreateWalkDustTrail();
            _animationTriggerCalled = false;
        }

        if (inputDir.sqrMagnitude <= 0.03f)
        {
            Controller.ChangeState(typeof(PlayerIdleState));
            return;
        }
        else
        {
            Player.Move(inputDir * Player.Data.movementSpeed);
        }
    }

    public override void ExitState()
    {
        Player.AnimatorCompo.SetBool(_movementHash, false);
        Player.InputReader.OnAttackEvent -= AttackHandle;
        Player.InputReader.OnAttackEndEvent -= AttackEndHandle;
        Player.InputReader.OnRollEvent -= RollHandle;
    }

    private void CreateWalkDustTrail()
    {
        var particle = PoolManager.Instance.Pop("WalkDustParticle") as PoolableParticle;
        var pos = Player.transform.position;
        var rot = Quaternion.LookRotation(-Player.ModelTrm.forward + Vector3.up);
        particle.SetPositionAndRotation(pos, rot);
        particle.Play();
    }

    private void MovementDirAnimatorParameterSet(Vector3 inputDir)
    {
        Player.AnimatorCompo.SetBool(_movementHash, true);
    }
}