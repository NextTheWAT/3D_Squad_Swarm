using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NPCDeathState : NPCBaseState 
{
    public NPCDeathState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (stateMachine.Npc.npcType == NPCType.VIP)
        {
            PlayerManager.Instance.PlayerSpeedUp();
            ZombieManager.Instance.AddGlobalSpeed(0.5f);
        }
        if(stateMachine.Npc.npcType == NPCType.Hunter)
        {
            var aim = stateMachine.Npc.GetComponent<GunAimer>();
            if (aim != null)
            {
                aim.EnableAim(false);
            }
        }
        stateMachine.Npc.gameObject.tag = "Untagged";
        stateMachine.Npc.agent.isStopped = true;
        PlayTriggerAnimation(stateMachine.Npc.AnimationData.deathParameterHash);

        stateMachine.Npc.Die();
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
    }


}
