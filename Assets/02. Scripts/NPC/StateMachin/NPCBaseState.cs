using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseState : IState
{
    protected NPCStateMachine stateMachine;

    public NPCBaseState(NPCStateMachine npcStateMachine)
    {
        this.stateMachine = npcStateMachine;
        //groundData = stateMachine.Player.Data.GroundData;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    void IState.Update()
    {
       
    }
}
