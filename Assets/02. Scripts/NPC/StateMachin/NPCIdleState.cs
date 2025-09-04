using UnityEngine;
using UnityEngine.AI;

public class NPCIdleState : NPCGroundState
{


    public NPCIdleState(NPCStateMachine npcStateMachine) : base(npcStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        //이동속도
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
            if (PlayerManager.Instance == null)
            {
                Debug.Log("플레이어매니저가 없음");
                return;
            }

            if (PlayerManager.Instance.player == null)
            {
                Debug.Log("플레이어가 없음");
                return;
            }

            if (PlayerManager.Instance.player.Stats == null)
            {
                Debug.Log("스탯이 없음");
                return;
            }

            float speed = PlayerManager.Instance.player.Stats.GetStat(StatType.Speed);
            if (speed == 0)
            {
                Debug.Log("스피드가 없음");
            }
            else
            {
                Debug.Log($"스피드 값: {speed}");
                stateMachine.Npc.agent.speed = speed;
            }
        }

            StartAnimation(stateMachine.Npc.AnimationData.WalkParameterHash);


        stateMachine.Npc.agent.isStopped = false; // NavMeshAgent 켜기
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();


        // 추격/도망 조건 확인
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
