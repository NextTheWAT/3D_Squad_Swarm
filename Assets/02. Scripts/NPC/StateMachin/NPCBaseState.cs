using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseState : IState
{
    protected NPCStateMachine stateMachine;

    public NPCBaseState(NPCStateMachine npcStateMachine)
    {
        this.stateMachine = npcStateMachine;
    }

    public void Enter()
    {
    }

    public void Exit()
    { 
    }

    public virtual void Update()
    {
        // StartAnimation 함수 먼저 작성
        Move();
    }

    public void HandleInput()
    {
    }

    public void PhysicsUpdate()
    {

    }
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Npc.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Npc.Animator.SetBool(animationHash, false);
    }
    private void Move()
    {
        // GetMoveMentDirection 함수 먼저 작성
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);

        // Rotate 함수 먼저 작성
        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Npc.transform.position);

       return dir;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.Npc.Controller.Move(((direction * movementSpeed) + stateMachine.Npc.ForceReceiver.Movement) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform NPCTransform = stateMachine.Npc.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            NPCTransform.rotation = Quaternion.Slerp(NPCTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected bool IsInChaseRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Npc.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Npc.Stats.GetStat(StatType.ChaseRange);
    }

   
}
