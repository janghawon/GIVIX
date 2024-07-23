using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EntityData/Player")]
public class PlayerData : EntityData
{
    [Header("Player Data")]
    public float attackMovementSpeed;
    public float attackTurm;
    public float rollDistance;
    public float rollDuration;
}
