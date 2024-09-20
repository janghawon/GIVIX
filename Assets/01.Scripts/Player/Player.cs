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
    public CursorConrtroller CursorController { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _stateController.ChangeState(EntityState.Idle);

        CursorController = GetComponent<CursorConrtroller>();
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
            Vector3 point = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            Vector3 dir = point - transform.position;
            dir.Normalize();
            Quaternion rotation = Quaternion.LookRotation(-dir);
            LookDir = rotation;
            Rotate(LookDir);
        }
    }
}
