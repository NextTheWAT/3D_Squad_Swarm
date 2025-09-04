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

    }

    public override void Exit()
    {
        base.Exit();
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
        LookTarget();

        if (Time.time >= lastFireTime + fireCooldown)
        {
            PlayTriggerAnimation(stateMachine.Npc.AnimationData.attackParameterHash);
            lastFireTime = Time.time;
        }


    }

    public void Shoot()
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
            var aim = npc.GetComponent<GunAimer>();
            if (aim != null)
            {
                aim.EnableAim(false);
            }
        }
    }
    private void LookTarget()
    {
        NPC npc = stateMachine.Npc;

        Vector3 targetDir = (stateMachine.Target.transform.position - npc.firePoint.position).normalized;
        Quaternion LookDir = Quaternion.LookRotation(targetDir);
        if (targetDir != Vector3.zero)
        {
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, LookDir, npc.Stats.GetStat(StatType.RotationDamping) * Time.deltaTime);
        }

        var aim = npc.GetComponent<GunAimer>();
        if (aim != null)
        {
            aim.EnableAim(true);
        }

    }



}

