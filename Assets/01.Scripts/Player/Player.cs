using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private PlayerInputReader _inputReader;
    public PlayerInputReader InputReader => _inputReader;

    [SerializeField] private float _layerTransitionTime = 0.1f;
    
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _enemyMask;

    public PlayerData PlayerData => (PlayerData)Data;

    public int PlayerAttackComboCounter { get; set; }
    private Vector3 _beforeDir;

    #region Gizmos Control Variable
    #if UNITY_EDITOR
    [Space(10)] [Header("For Gizmos")]
    [SerializeField] private bool _drawAttackRange;
    [SerializeField] private bool _drawBlockRange;
    #endif    
    #endregion

    public override void Awake()
    {
        base.Awake();
        PlayerAttackComboCounter = 0;
        StateController.RegisterState(new PlayerIdleState(StateController, "Idle"));
        StateController.RegisterState(new PlayerMovementState(StateController, "Movement"));
        StateController.RegisterState(new PlayerAttackState(StateController, "Attack"));
        StateController.RegisterState(new PlayerRollState(StateController, "Roll"));
        StateController.RegisterState(new PlayerAttackRunState(StateController, ""));
        StateController.ChangeState(typeof(PlayerIdleState));
    }

    public override void Update()
    {
        base.Update();
        LookPointDir();
    }

    private void LookPointDir()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y;
            
            transform.LookAt(Vector3.Lerp(_beforeDir, targetPosition, Time.deltaTime));
            _beforeDir = targetPosition;
        }
    }

    public override void OnDamage(float damage, Vector3 attackedDir)
    {
        base.OnDamage(damage, attackedDir);
    }

    public List<IDamageable> GetDamageableObjects(out List<Vector3> points)
    {
        var cols = new Collider[PlayerData.maxAttackCount];
        var result = new List<IDamageable>();
        var center = transform.position + CharacterControllerCompo.center + ModelTrm.forward * PlayerData.attackDistance;
        var count = Physics.OverlapSphereNonAlloc(center, PlayerData.attackRadius, cols, _enemyMask);

        points = new List<Vector3>();
        for (var i = 0; i < count; i++)
        {
            if (cols[i].TryGetComponent<IDamageable>(out var damageable))
            {
                points.Add(cols[i].ClosestPointOnBounds(center));
                result.Add(damageable);
            }
        }

        return result;
    }
    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (Data is null)
        {
            return;
        }

        if (_drawAttackRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                transform.position + GetComponent<CharacterController>().center +
                transform.Find("Model").forward * PlayerData.attackDistance,
                PlayerData.attackRadius
            );
        }
    }

#endif
}