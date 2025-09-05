using UnityEngine;
using UnityEngine.AI;

public class NPCIdleState : NPCGroundState
{


    public NPCIdleState(NPCStateMachine npcStateMachine) : base(npcStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        //�̵��ӵ�
        if (stateMachine.Npc.npcType == NPCType.Hunter)
        {
            stateMachine.Npc.agent.speed = 3;
        }
        else if (stateMachine.Npc.npcType == NPCType.Civilian)
        {
            stateMachine.Npc.agent.speed = 2;
        }
        else if (stateMachine.Npc.npcType == NPCType.VIP) 
        {
            float speed = PlayerManager.Instance.player.Stats.GetStat(StatType.Speed);   
        }

            StartAnimation(stateMachine.Npc.AnimationData.WalkParameterHash);


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


        // �߰�/���� ���� Ȯ��
        if (stateMachine.Npc.npcType == NPCType.Hunter && IsInDetectRange())
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
