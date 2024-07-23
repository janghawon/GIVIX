using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerBaseCompoElementGroup
{
    public LayerMask groundMask;
}

public partial class Player : Entity
{
    [Header("Player Base")]
    [SerializeField] private PlayerBaseCompoElementGroup _baseElementGroup;

    public Quaternion LookDir { get; private set; } 

    protected override void Awake()
    {
        base.Awake();
        _stateController.ChangeState(EntityState.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (IsDead()) return;

        LookPointer();
        CreateCartridgeParticle();
    }

    private void LookPointer()
    {
        Vector2 screenPos = _inputReader.MouseInput;
        Ray cameraRay = Camera.main.ScreenPointToRay(screenPos);
        float rayDistance = Camera.main.farClipPlane;

        bool isHit = Physics.Raycast(cameraRay, out var hit, rayDistance, _baseElementGroup.groundMask);

        if (isHit)
        {
            Vector3 dir;
            
            if(_inputReader.IsOnAttack)
            {
                //Vector3 point = hit.point;
                //point.y = _attackElementGroup.fireTrm.position.y;

                //dir = point - _attackElementGroup.fireTrm.position;
                dir = _attackElementGroup.fireTrm.forward;
            }
            else
            {
                dir = hit.point - transform.position;
            }

            dir.Normalize();

            Quaternion rotation = Quaternion.LookRotation(dir);

            LookDir = rotation;

            Rotate(LookDir);
        }
    }
}
