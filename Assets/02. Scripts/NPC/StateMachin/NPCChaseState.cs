using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChaseState : NPCGroundState
{
    public NPCChaseState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1f; // Run = full speed
        stateMachine.RotationDampingModifier = 1f;
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
        ChaseTarget();
        if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

        if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }

    private void ChaseTarget()
    {
        Vector3 targetPos = stateMachine.Target.transform.position;
        stateMachine.Npc.agent.isStopped = false;  // 이동 가능
        stateMachine.Npc.agent.SetDestination(targetPos);
    }

}
