using UnityEngine;

public class NPCStateMachine : StateMachine
{
    public npc Npc { get; }
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }

    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public GameObject Target { get; private set; }
    public NPCIdleState IdleState { get; }
    public NPCChaseState ChaseState { get; }
    public NPCAttackState AttackState { get; }

    //public NPCStatMachine(NPC npc)
    //{
    //    this.Npc = npc;
    //    Target = GameObject.FindGameObjectWithTag("Player");

    //    IdleState = new NPCIdleState(this);
    //    ChaseState = new NPCChaseState(this);
    //    AttackState = new NPCAttackState(this);

    //    //MovementSpeed = npc.Data.GroundData.BaseSpeed;
    //    //RotationDamping = npc.Data.GroundData.BaseRotationDamping;

    //}

}