using UnityEngine;

public class ZombieIdleState : ZombieBaseState
{
    public ZombieIdleState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        stateMachine.Zombie.Agent.isStopped = true;
        stateMachine.Zombie.Agent.speed = PlayerManager.Instance.player.Stats.GetStat(StatType.Speed);
        StartAnimation(stateMachine.Zombie.animationData.IdleParameterHash);
    }

    public override void Update()
    {
        var zombie = stateMachine.Zombie;

        // --- 1. Enemy check first ---
        if (zombie.EnemyTarget == null)
            zombie.EnemyTarget = zombie.FindClosestEnemy();

        if (zombie.EnemyTarget != null)
        {
            float distSqr = (zombie.EnemyTarget.position - zombie.transform.position).sqrMagnitude;
            if (distSqr <= stateMachine.DetectionRange * stateMachine.DetectionRange)
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                zombie.EnemyTarget = null;
            }
        }

        // --- 2. Follow player if too far ---
        float distToPlayerSqr = (zombie.PlayerTarget.position - zombie.transform.position).sqrMagnitude;
        if (distToPlayerSqr > zombie.followRange * zombie.followRange)
        {
            stateMachine.ChangeState(stateMachine.FollowState);
        }
    }


    public override void Exit()
    {
        StopAnimation(stateMachine.Zombie.animationData.IdleParameterHash);
        stateMachine.Zombie.Agent.isStopped = false;
    }
}
