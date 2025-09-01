using UnityEngine;

public class ZombieAttackState : ZombieBaseState
{
    public ZombieAttackState(ZombieStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Zombie.animationData.attackParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Zombie.animationData.attackParameterHash);
    }
}
