using UnityEngine;

[System.Serializable]
public class UnitStats
{
    [Header("Combat")]
    public float maxHP = 100f;
    public float attack = 10f;
    public float defense = 0f;

    [Header("Movement")]
    public float moveSpeed = 3.5f;

    [Header("Attack Timing")]
    [Tooltip("초당 공격 횟수(= 1 / AttackIntervalSec)")]
    public float attackPerSec = 1f;

    public float AttackIntervalSec => attackPerSec <= 0f ? 999f : 1f / attackPerSec;
}
