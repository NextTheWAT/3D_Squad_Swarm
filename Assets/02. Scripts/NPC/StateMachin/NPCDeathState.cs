using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeathState : NPCBaseState 
{
    public NPCDeathState(NPCStateMachine npcStateMachine) : base(npcStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ав╬З╢ы"!);
        stateMachine.Npc.agent.isStopped = true;
        PlayTriggerAnimation(stateMachine.Npc.AnimationData.deathParameterHash);
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
