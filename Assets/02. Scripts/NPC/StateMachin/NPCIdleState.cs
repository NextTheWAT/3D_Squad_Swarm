using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCBaseState
{
    public float patrolRange = 10f;
    public NPCIdleState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Npc.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (!stateMachine.Npc.agent.pathPending && stateMachine.Npc.agent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
        //if (stateMachine.Npc.npcType == NPCType.Hunter&&IsInChaseRange())
        //{
        //    stateMachine.ChangeState(stateMachine.ChaseState);
        //    return;
        //}
        //else
        //{
        //    stateMachine.ChangeState(stateMachine.FleeState);
        //    return;
        //}

    }
    void SetRandomDestination()
    {
        Vector3 randomPos = stateMachine.Npc.transform.position + new Vector3(
            Random.Range(-patrolRange, patrolRange),
            0,
            Random.Range(-patrolRange, patrolRange)
        );
        stateMachine.Npc.agent.SetDestination(randomPos);
    }


}
