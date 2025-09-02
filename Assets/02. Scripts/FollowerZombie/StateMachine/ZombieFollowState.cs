using UnityEngine;
using UnityEngine.AI;

public class ZombieFollowState : ZombieBaseState
{
    private NavMeshAgent agent;
    private float minDistance = 1.2f;          // minimum spacing between zombies
    private float separationStrength = 2.0f;   // how strongly they push apart

    public ZombieFollowState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        agent = stateMachine.Zombie.Agent;

        // Random stopping distance for variety
        agent.stoppingDistance = Random.Range(1.0f, 2.5f);

        StartAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }

    public override void Update()
    {
        Transform leader = stateMachine.Zombie.PlayerTarget;
        if (leader == null) return;

        // --- Base movement toward leader ---
        Vector3 targetPos = leader.position;

        // --- Separation from other zombies ---
        Vector3 separation = Vector3.zero;
        Collider[] nearby = Physics.OverlapSphere(agent.transform.position, minDistance);
        foreach (Collider col in nearby)
        {
            if (col != null && col.gameObject != agent.gameObject && col.CompareTag("Zombie"))
            {
                Vector3 away = agent.transform.position - col.transform.position;
                float distance = away.magnitude;

                if (distance > 0.01f) // prevent AABB Error
                {
                    separation += away.normalized / distance; 
                }
            }
        }

        // Apply separation force
        if (separation != Vector3.zero)
        {
            targetPos += separation * separationStrength;
        }

        // --- Ensure position is valid before moving ---
        if (targetPos.x == Mathf.Infinity || targetPos.y == Mathf.Infinity || targetPos.z == Mathf.Infinity)
            return;

        agent.SetDestination(targetPos);

        // --- Animation control: only play walk when actually moving ---
        if (agent.velocity.sqrMagnitude > 0.05f)
        {
            StartAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
        }
        else
        {
            StopAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
        }
    }

    public override void Exit()
    {
        StopAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }
}
