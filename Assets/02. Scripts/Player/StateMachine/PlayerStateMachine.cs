using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public bool IsAttacking { get; set; }

    //States 
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float RotationDampingModifier { get; set; } = 1f;
    //public float JumpForceModifier { get; set; } = 1f;
    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        AttackState = new PlayerAttackState(this);

        MainCameraTransform = Camera.main.transform;
    }
    public float MovementSpeed => Player.Stats.GetStat(StatType.Speed) * MovementSpeedModifier;
    public float RotationDamping => Player.Stats.GetStat(StatType.RotationDamping) * RotationDampingModifier;
    //public float JumpForce => Player.Stats.GetStat(StatType.JumpPower) * JumpForceModifier;
}