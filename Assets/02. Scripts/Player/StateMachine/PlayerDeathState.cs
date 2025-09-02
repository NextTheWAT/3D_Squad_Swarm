using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine sm) : base(sm) { }

    public override void Enter()
    {
        // Stop all movement
        stateMachine.MovementInput = Vector2.zero;

        if (stateMachine.Player.Controller != null)
        {
            stateMachine.Player.Controller.Move(Vector3.zero);
        }
        // Trigger death animation
        PlayTriggerAnimation(stateMachine.Player.AnimationData.deathParameterHash);
    }


    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
