using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieBaseState
{
    public ZombieIdleState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        // Stop movement
        stateMachine.Zombie.Agent.isStopped = true;

        // Play idle animation
        StartAnimation(stateMachine.Zombie.animationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Zombie.animationData.IdleParameterHash);
        stateMachine.Zombie.Agent.isStopped = false;
    }
}
