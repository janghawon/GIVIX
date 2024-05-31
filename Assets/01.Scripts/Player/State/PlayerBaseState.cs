public abstract class PlayerBaseState : State
{
    protected Player Player => (Player)Owner;
    
    public PlayerBaseState(StateController controller, string animationParameter) : base(controller, animationParameter)
    {
    }

    protected virtual void RollHandle()
    {
        Controller.ChangeState(typeof(PlayerRollState));
    }

    protected virtual void AttackHandle()
    {
        Controller.ChangeState(typeof(PlayerAttackState));
    }

    protected virtual void AttackEndHandle()
    {
        Controller.ChangeState(typeof(PlayerIdleState));
    }
}