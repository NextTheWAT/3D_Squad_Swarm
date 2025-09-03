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
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.Stats.BoostStatRound(StatType.Speed, 0.5f);
            Debug.Log("Player Speed:" + player.Stats.GetStat(StatType.Speed));
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
