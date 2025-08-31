using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float JumpForceModifier { get; set; } = 1f;
    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        MainCameraTransform = Camera.main.transform;
    }
    public float MovementSpeed => Player.Stats.GetStat(StatType.Speed) * MovementSpeedModifier;
    public float RotationDamping => Player.Stats.GetStat(StatType.RotationDamping);
    public float JumpForce => Player.Stats.GetStat(StatType.JumpPower) * JumpForceModifier;
}