using UnityEngine;

public class ZombieRiseState : ZombieBaseState
{
    public ZombieRiseState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        stateMachine.Zombie.Agent.isStopped = true;
        StartAnimation(stateMachine.Zombie.animationData.riseParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        stateMachine.Zombie.Agent.isStopped = false;
        StopAnimation(stateMachine.Zombie.animationData.riseParameterHash);
    }
}
