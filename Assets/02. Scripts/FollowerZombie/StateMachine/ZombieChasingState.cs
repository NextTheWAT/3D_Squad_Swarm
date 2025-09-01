using UnityEngine;

public class ZombieChasingState : ZombieBaseState
{
    public ZombieChasingState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        // Start run animation (assuming you have a RunParameterHash)
        StartAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }

    public override void Update()
    {
        var enemy = stateMachine.Zombie.EnemyTarget;
        if (enemy == null)
        {
            // No enemy to chase → stop moving
            StopMoving();
            return;
        }

        // Move toward the enemy
        MoveTo(enemy.position);

        // If enemy moved out of detection range, stop chasing
        if (!IsEnemyInDetectionRange())
        {
            StopMoving();
        }
    }

    public override void Exit()
    {
        // Stop run animation
        StopAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }
}
