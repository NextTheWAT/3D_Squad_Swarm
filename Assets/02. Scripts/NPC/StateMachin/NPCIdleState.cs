using UnityEngine;
using UnityEngine.AI;

public class NPCIdleState : NPCGroundState
{
    private float idleTime = 2f; // �ּ� ��� �ð�
    private float timer;

    public NPCIdleState(NPCStateMachine npcStateMachine) : base(npcStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Npc.AnimationData.WalkParameterHash);

        timer = 0f;
        stateMachine.Npc.agent.isStopped = false; // NavMeshAgent �ѱ�
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        // �߰�/���� ���� Ȯ��
        if (stateMachine.Npc.npcType == NPCType.Hunter && IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }
        else if (stateMachine.Npc.npcType != NPCType.Hunter && IsInDetectRange())
        {
            stateMachine.ChangeState(stateMachine.FleeState);
            return;
        }

        
    }
}
