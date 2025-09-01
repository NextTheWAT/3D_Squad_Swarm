using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChaseState : NPCBaseState
{
    public NPCChaseState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1f; // Run = full speed
        stateMachine.RotationDampingModifier = 1f;
        base.Enter();
        StartAnimation(stateMachine.Npc.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (isInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    protected bool isInAttackRange() 
    {

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Npc.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Npc.Stats.GetStat(StatType.NPCAttackRange)* stateMachine.Npc.Stats.GetStat(StatType.NPCAttackRange);
    }
}
