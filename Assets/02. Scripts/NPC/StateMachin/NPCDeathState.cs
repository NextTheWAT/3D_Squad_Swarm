using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDeathState : NPCBaseState 
{
    public NPCDeathState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ав╬З╢ы!");
        if(stateMachine.Npc.npcType == NPCType.VIP)
        {
            var player = stateMachine.Target.GetComponent<Player>();
            //player.Stats.BoostStat(StatType.Speed, 20);
        }
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
