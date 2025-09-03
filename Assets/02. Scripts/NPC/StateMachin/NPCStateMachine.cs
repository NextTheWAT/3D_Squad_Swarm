using UnityEngine;

public class NPCStateMachine : StateMachine
{
    public NPC Npc { get; }

    public float MovementSpeedModifier { get; set; } = 1f;
    public float RotationDampingModifier { get; set; } = 1f;
    public bool IsDead { get; private set; } = false;

    public GameObject Target { get; private set; }
    public NPCIdleState IdleState { get; }
    public NPCChaseState ChaseState { get; }
    public NPCAttackState AttackState { get; }

    public NPCFleeState FleeState { get; }
    public NPCDeathState DeathState { get; }



    public NPCStateMachine(NPC npc)
    {
        this.Npc = npc;
        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new NPCIdleState(this);
        ChaseState = new NPCChaseState(this);
        AttackState = new NPCAttackState(this);
        FleeState = new NPCFleeState(this);
        DeathState = new NPCDeathState(this);

    }
    public void SetDead()
    {
        IsDead = true;
        ChangeState(DeathState);
    }

    public float MovementSpeed => Npc.Stats.GetStat(StatType.Speed) * MovementSpeedModifier;
    public float RotationDamping => Npc.Stats.GetStat(StatType.RotationDamping) * RotationDampingModifier;

}