using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInputs { get; private set; }
    public PlayerInput.PlayerMovementActions playerActions { get; private set; }
    private void Awake()
    {
        playerInputs = new PlayerInput();
        playerActions = playerInputs.PlayerMovement;
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
