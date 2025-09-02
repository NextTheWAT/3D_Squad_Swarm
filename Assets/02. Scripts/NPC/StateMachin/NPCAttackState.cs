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
        StartAnimation(stateMachine.Npc.AnimationData.attackParameterHash);

        //���� ���ؽð��� �ְ�ʹٸ� lastFireTime = Time.time; �ٵ� �̷��� �ִϸ��̼��� Ʈ���ŷ� �̺�Ʈȣ���ؾ��ҰŰ���
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.attackParameterHash);
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

            // �Ѿ� ����
            GameObject bullet = GameObject.Instantiate(npc.bulletPrefab, npc.firePoint.position, Quaternion.LookRotation(targetDir));

            // �Ѿ˿� ���� ���� ������ ���ư��� ��
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = targetDir * npc.bulletSpeed;
            }

            Debug.Log("�Ѿ� �߻�");
        }
    }

   
}

