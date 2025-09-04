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
        stateMachine.Npc.agent.isStopped = true; //���ݽ� ���߰�
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Npc.agent.isStopped = false; //������ ������
    }

    public override void Update()
    {
        base.Update();
        //�ѽ�� ���� �ۼ�

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

            // �Ѿ� ����
            GameObject bullet = GameObject.Instantiate(npc.bulletPrefab, npc.firePoint.position, Quaternion.LookRotation(targetDir));

            // �Ѿ˿� ���� ���� ������ ���ư��� ��
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

