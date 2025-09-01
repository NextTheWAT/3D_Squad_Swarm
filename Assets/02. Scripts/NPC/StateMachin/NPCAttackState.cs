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
        stateMachine.MovementSpeedModifier = 0f;
        stateMachine.RotationDampingModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Npc.AnimationData.attackParameterHash);

        lastFireTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        //ÃÑ½î´Â ·ÎÁ÷ ÀÛ¼º

        if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        if (IsInChaseRange()&&Time.time >= lastFireTime + fireCooldown)
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
            // ÃÑ¾Ë »ý¼º
            GameObject bullet = GameObject.Instantiate(npc.bulletPrefab, npc.firePoint.position, npc.firePoint.rotation);

            // ÃÑ¾Ë¿¡ ÈûÀ» °¡ÇØ ¾ÕÀ¸·Î ³¯¾Æ°¡°Ô ÇÔ
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = npc.firePoint.forward * npc.bulletSpeed;
            }

            Debug.Log("ÃÑ¾Ë ¹ß»ç");
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Npc.AnimationData.attackParameterHash);
    }
}

