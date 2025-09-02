using UnityEngine;

public class ZombieBaseState : IState
{
    protected readonly ZombieStateMachine stateMachine;

    public ZombieBaseState(ZombieStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void HandleInput() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Update() { }

    // -----------------------------
    // Animation helpers
    // -----------------------------
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Zombie.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Zombie.Animator.SetBool(animationHash, false);
    }
    protected void PlayTriggerAnimation(int triggerHash)
    {
        stateMachine.Zombie.Animator.SetTrigger(triggerHash);
    }

    // -----------------------------
    // Movement helpers (NavMesh)
    // -----------------------------
    protected void MoveTo(Vector3 destination)
    {
        if (stateMachine.Zombie.Agent.isActiveAndEnabled)
        {
            stateMachine.Zombie.Agent.isStopped = false;
            stateMachine.Zombie.Agent.speed = stateMachine.MovementSpeed;
            stateMachine.Zombie.Agent.angularSpeed = stateMachine.RotationDamping * 100f;
            // (Unity angularSpeed is in degrees/sec → scale as needed)

            stateMachine.Zombie.Agent.SetDestination(destination);
        }
    }

    protected void StopMoving()
    {
        if (stateMachine.Zombie.Agent.isActiveAndEnabled)
        {
            stateMachine.Zombie.Agent.isStopped = true;
            stateMachine.Zombie.Agent.velocity = Vector3.zero;
        }
    }

    // -----------------------------
    // Target helpers
    // -----------------------------
    protected bool IsEnemyInDetectionRange()
    {
        if (stateMachine.Zombie.EnemyTarget == null) return false;

        float distSqr = (stateMachine.Zombie.EnemyTarget.position - stateMachine.Zombie.transform.position).sqrMagnitude;
        return distSqr <= stateMachine.DetectionRange * stateMachine.DetectionRange;
    }

    protected bool IsNearPlayer(float followRange)
    {
        if (stateMachine.Zombie.PlayerTarget == null) return false;

        float distSqr = (stateMachine.Zombie.PlayerTarget.position - stateMachine.Zombie.transform.position).sqrMagnitude;
        return distSqr <= followRange * followRange;
    }
}
