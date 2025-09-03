using UnityEngine;

public class ZombieChasingState : ZombieBaseState
{
    public ZombieChasingState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1f;
        stateMachine.RotationDampingModifier = 1f;
        StartAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }

    public override void Update()
    {
        var zombie = stateMachine.Zombie;
        var enemy = zombie.EnemyTarget;

        if (enemy == null)
        {
            StopMoving();
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        // Move toward enemy
        MoveTo(enemy.position);

        // Stop chasing if enemy out of range
        if (!IsEnemyInDetectionRange())
        {
            zombie.EnemyTarget = null;
            StopMoving();
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }
}
