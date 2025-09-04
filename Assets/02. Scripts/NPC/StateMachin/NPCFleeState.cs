using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFleeState : NPCGroundState
{
    public NPCFleeState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }
    public override void Enter()
    {
        if (stateMachine.Npc.npcType == NPCType.Civilian)
        {
            stateMachine.Npc.agent.speed = 3;
        }
        else if (stateMachine.Npc.npcType == NPCType.VIP)
        {
            stateMachine.Npc.agent.speed = PlayerManager.Instance.player.Stats.GetStat(StatType.Speed);
        }
        base.Enter();
        StartAnimation(stateMachine.Npc.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();
        FleeFromTarget();

        if (!IsInDetectRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void FleeFromTarget()
    {
        Vector3 dir = (stateMachine.Npc.transform.position - stateMachine.Target.transform.position).normalized;
        Vector3 fleePos = stateMachine.Npc.transform.position + dir * 10f;

        stateMachine.Npc.agent.SetDestination(fleePos);
    }
 

}
