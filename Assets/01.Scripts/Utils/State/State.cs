using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected EntityData _data;
    protected Entity _owner;
    protected StateController _stateController;
    protected InputReader _reader;

    public State(EntityState state, EntityData data, StateController controller, Entity target)
    {
        _data = data;
        _stateController = controller;
        _reader = controller.inputReader;
        _owner = target;

        controller.SetupState(state, this, data);
    }

    protected T GetDataType<T>(EntityData data) where T : EntityData
    {
        return data as T;
    }

    protected void ChangeState(EntityState toChangeState)
    {
        _stateController.ChangeState(toChangeState);
    }
    
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
