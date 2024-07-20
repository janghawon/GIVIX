using System.Collections;
using System.Collections.Generic;

public class StateController 
{
    private Dictionary<EntityState, State> _entityStateDic = new();
    private State _currentState;
    public InputReader inputReader;

    public State CurrentState => _currentState;
    
    public bool OnControlling { get; private set; }

    public StateController(InputReader reader)
    {
        inputReader = reader;
        _entityStateDic = new Dictionary<EntityState, State>();
    }

    public void SetupState(EntityState e_state, State state, EntityData data)
    {
        _entityStateDic.Add(e_state, state);
    }

    public void EndState()
    {
        _currentState?.Exit();
        OnControlling = false;
    }

    public void ChangeState(EntityState toChangeState)
    {
        OnControlling = true;

        _currentState?.Exit();
        _currentState = _entityStateDic[toChangeState];
        _currentState.Enter();
    }
}
