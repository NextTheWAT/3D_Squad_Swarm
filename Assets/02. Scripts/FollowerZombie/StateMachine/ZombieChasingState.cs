using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ZombieChasingState : ZombieBaseState
{
    public ZombieChasingState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1f;
        stateMachine.RotationDampingModifier = 1f;
        stateMachine.Zombie.Agent.speed = PlayerManager.Instance.player.Stats.GetStat(StatType.Speed);
        StartAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }

    public override void Update()
    {
        var zombie = stateMachine.Zombie;
        var enemy = zombie.EnemyTarget;

        if (enemy == null)
        {
            Debug.Log("Testing123");
            StopMoving();
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        // ChargingZombie check: if within charge distance, switch to ChargeState
        if (zombie is ChargingZombie && stateMachine.ChargeState != null)
        {
            float distToEnemy = Vector3.Distance(zombie.transform.position, enemy.position);
            if (distToEnemy <= stateMachine.ChargeDistance)
            {
                Debug.Log("Charging");
                stateMachine.ChangeState(stateMachine.ChargeState);
                return;
            }
        }

        // Normal chasing movement
        MoveTo(enemy.position);

        // Stop chasing if enemy out of detection range
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
