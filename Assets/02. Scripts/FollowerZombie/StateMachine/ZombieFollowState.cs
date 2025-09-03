using UnityEngine;
using UnityEngine.AI;

public class ZombieFollowState : ZombieBaseState
{
    private NavMeshAgent agent;
    private float minDistance = 1.2f;          
    private float separationStrength = 2.0f;   

    public ZombieFollowState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        agent = stateMachine.Zombie.Agent;
        agent.stoppingDistance = Random.Range(1.0f, 2.5f);
        StartAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
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

        // --- 2. Follow player ---
        Transform leader = zombie.PlayerTarget;
        if (leader == null) return;

        Vector3 targetPos = leader.position;

        // Separation from other zombies
        Vector3 separation = Vector3.zero;
        Collider[] nearby = Physics.OverlapSphere(agent.transform.position, minDistance);
        foreach (Collider col in nearby)
        {
            if (col != null && col.gameObject != agent.gameObject && col.CompareTag("Zombie"))
            {
                Vector3 away = agent.transform.position - col.transform.position;
                float distance = away.magnitude;
                if (distance > 0.01f)
                    separation += away.normalized / distance;
            }
        }

        targetPos += separation * separationStrength;
        agent.SetDestination(targetPos);

        // Switch to idle if close enough
        if ((leader.position - zombie.transform.position).sqrMagnitude <= zombie.followRange * zombie.followRange)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        // Walk animation
        if (agent.velocity.sqrMagnitude > 0.01f)
            StartAnimation(zombie.animationData.WalkParameterHash);
        else
            StopAnimation(zombie.animationData.WalkParameterHash);
    }


    public override void Exit()
    {
        StopAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }
}
