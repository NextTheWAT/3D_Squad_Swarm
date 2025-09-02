using UnityEngine;

public class ZombieDeathState : ZombieBaseState
{
    public ZombieDeathState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        // Stop movement
        if (stateMachine.Zombie.Agent != null)
        {
            stateMachine.Zombie.Agent.isStopped = true;
            stateMachine.Zombie.Agent.velocity = Vector3.zero;
        }

        // Trigger death animation
        PlayTriggerAnimation(stateMachine.Zombie.animationData.deathParameterHash);
    }

    public override void Update()
    {
        // No need for update; handled by animation event
    }

    public override void Exit()
    {
        // Nothing to reset
    }
}
