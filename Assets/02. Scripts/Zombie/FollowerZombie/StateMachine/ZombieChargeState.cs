using UnityEngine;

public class ZombieChargeState : ZombieBaseState
{
    public ZombieChargeState(ZombieStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // Double speed while charging
        stateMachine.MovementSpeedModifier = 2f;
        Debug.Log("ChargeAnimation");
        StartAnimation(stateMachine.Zombie.animationData.chargeParameterHash);
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

        // Keep moving toward enemy
        MoveTo(enemy.position);

        // Stop charge if enemy out of detection range
        if (!IsEnemyInDetectionRange())
        {
            zombie.EnemyTarget = null;
            StopMoving();
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void Exit()
    {
        // Reset speed
        stateMachine.MovementSpeedModifier = 1f;

        // Stop charge animation
        StopAnimation(stateMachine.Zombie.animationData.chargeParameterHash);
    }
}
