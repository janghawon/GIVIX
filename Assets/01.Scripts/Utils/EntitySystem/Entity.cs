using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public abstract class Entity : PoolableMono
{
    [Header("Entity Base")]
    [SerializeField] protected EntityData _entityData;
    [SerializeField] protected InputReader _inputReader;
    [SerializeField] protected Transform _visualTrm;
    public Transform VisualTrm => _visualTrm;

    protected StateController _stateController;

    protected Animator _animator;
    public Animator Animator => _animator;

    protected CharacterController _characterController;
    public CharacterController CharacterController => _characterController;

    protected Rigidbody _rigidBody;
    public Rigidbody Rigidbody => _rigidBody;

    public override void OnPop()
    {
    }

    public override void OnPush()
    {
    }

    public void Rotate(Quaternion targetRot, float rotSpeed = -1)
    {
        float speed = rotSpeed < 0 ? _entityData.rotateSpeed : rotSpeed;

        transform.rotation = 
        Quaternion.Lerp
        (
            transform.rotation, 
            targetRot, 
            speed * Time.deltaTime
        );
    }

    protected virtual void Awake()
    {
        _stateController = new StateController(_inputReader);

        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        _rigidBody = GetComponentInChildren<Rigidbody>();

        var myClassName = this.GetType().Name;

        foreach (var stateType in Enum.GetValues(typeof(EntityState)))
        {
            Type type = Type.GetType($"{myClassName}{stateType}State");

            if (type == null) continue;

            var parameters = new object[4] { stateType, _entityData, _stateController, this };
            Activator.CreateInstance(type, parameters);
        }
    }

    protected virtual void Update()
    {
        if(_stateController.OnControlling)
        {
            _stateController.CurrentState.Update();
        }
    }

    public void Move(Vector3 velocity)
    {
        CharacterController.Move(velocity * Time.deltaTime);
    }
}
