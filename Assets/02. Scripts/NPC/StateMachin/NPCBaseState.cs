using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseState : IState
{
    protected NPCStateMachine stateMachine;
    public float patrolRange = 10f;

    public NPCBaseState(NPCStateMachine npcStateMachine)
    {
        this.stateMachine = npcStateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    { 
    }

    public virtual void Update()
    {
        if (!stateMachine.Npc.agent.pathPending && stateMachine.Npc.agent.remainingDistance < 0.5f)
        {
            Wandering();
        }
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {

    }
    protected virtual void StartAnimation(int animationHash)
    {
        stateMachine.Npc.Animator.SetBool(animationHash, true);
    }

    protected virtual void StopAnimation(int animationHash)
    {
        stateMachine.Npc.Animator.SetBool(animationHash, false);
    }
    protected void PlayTriggerAnimation(int triggerHash)
    {
        stateMachine.Npc.Animator.SetTrigger(triggerHash);
    }

    public void Wandering()
    {
        Vector3 randomPos = stateMachine.Npc.transform.position + new Vector3(
            Random.Range(-patrolRange, patrolRange),
            0,
            Random.Range(-patrolRange, patrolRange)
        );
        stateMachine.Npc.agent.SetDestination(randomPos);
    }

    protected bool IsInChaseRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Npc.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Npc.Stats.GetStat(StatType.ChaseRange) * stateMachine.Npc.Stats.GetStat(StatType.ChaseRange);
    }
    protected bool IsInDetectRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Npc.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Npc.Stats.GetStat(StatType.DetectRange) * stateMachine.Npc.Stats.GetStat(StatType.DetectRange);
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Npc.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Npc.Stats.GetStat(StatType.NPCAttackRange) * stateMachine.Npc.Stats.GetStat(StatType.NPCAttackRange);
    }


}
