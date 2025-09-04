using UnityEngine;

public class ZombieChargeState : ZombieBaseState
{
    private Vector3 chargeDirection;
    private float chargeSpeed = 13f;
    private float windupTime = 0.5f;
    private float chargeDuration = 1.0f;

    private float elapsedTime;
    private bool isCharging;

    public ZombieChargeState(ZombieStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        elapsedTime = 0f;
        isCharging = false;

        // pick a direction (example: towards enemy or forward)
        if (stateMachine.Zombie.EnemyTarget != null)
        {
            chargeDirection = (stateMachine.Zombie.EnemyTarget.position - stateMachine.Zombie.transform.position).normalized;
            chargeDirection.y = 0f;
        }
        else
        {
            chargeDirection = stateMachine.Zombie.transform.forward;
        }

        // play charge animation
        StartAnimation(stateMachine.Zombie.animationData.chargeParameterHash);

        // stop NavMesh movement during wind-up
        StopMoving();
    }

    public override void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!isCharging && elapsedTime >= windupTime)
        {
            // Begin dash
            isCharging = true;
            elapsedTime = 0f; // restart timer for charge duration
        }

        if (isCharging)
        {
            // Move manually instead of NavMesh
            stateMachine.Zombie.transform.position += chargeDirection * chargeSpeed * Time.deltaTime;

            if (elapsedTime >= chargeDuration)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Zombie.animationData.chargeParameterHash);
        // re-enable NavMesh for normal movement
        if (stateMachine.Zombie.Agent != null && stateMachine.Zombie.Agent.isActiveAndEnabled)
            stateMachine.Zombie.Agent.isStopped = false;
    }
}
