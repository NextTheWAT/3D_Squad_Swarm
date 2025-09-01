using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInputs { get; private set; }
    public PlayerInput.PlayerMovementActions playerActions { get; private set; }
    public Player player { get; private set; }
    private void Awake()
    {
        playerInputs = new PlayerInput();
        playerActions = playerInputs.PlayerMovement;
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            player.OnAttackHit();
            player.stateMachine.ChangeState(player.stateMachine.AttackState);
        }
    }
}
