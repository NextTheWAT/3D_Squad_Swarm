using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackState : NPCBaseState
{
    private float fireCooldown = 3.0f;
    private float lastFireTime;

    public NPCAttackState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Npc.agent.isStopped = true; //공격시 멈추고
        base.Enter();
        StartAnimation(stateMachine.Npc.AnimationData.attackParameterHash);

        //만약 조준시간을 주고싶다면 lastFireTime = Time.time; 근데 이러면 애니메이션을 트리거로 이벤트호출해야할거같음
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.attackParameterHash);
        stateMachine.Npc.agent.isStopped = false; //끝나면 움직임
    }

    public override void Update()
    {
        base.Update();
        //총쏘는 로직 작성

        if (!IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }

        if (Time.time >= lastFireTime + fireCooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }


    }

    private void Shoot()
    {
        NPC npc = stateMachine.Npc;

        if (npc.bulletPrefab != null && npc.firePoint != null)
        {
            Vector3 targetDir = (stateMachine.Target.transform.position - npc.firePoint.position).normalized;

            // 총알 생성
            GameObject bullet = GameObject.Instantiate(npc.bulletPrefab, npc.firePoint.position, Quaternion.LookRotation(targetDir));

            // 총알에 힘을 가해 앞으로 날아가게 함
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = targetDir * npc.bulletSpeed;
            }

            Debug.Log("총알 발사");
        }
    }

   
}

