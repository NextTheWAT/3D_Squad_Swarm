using UnityEngine;

public class ZombieFollowState : ZombieBaseState
{
    public ZombieFollowState(ZombieStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        // Play walk animation
        StartAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }

    public override void Update()
    {
        var player = stateMachine.Zombie.PlayerTarget;
        if (player == null) return;

        // Move toward player
        MoveTo(player.position);

        // If already close enough → stop moving (Idle will be triggered by FollowerZombie.Update)
        if (IsNearPlayer(stateMachine.Zombie.followRange))
        {
            StopMoving();
        }
    }

    public override void Exit()
    {
        // Stop walk animation
        StopAnimation(stateMachine.Zombie.animationData.WalkParameterHash);
    }
}
