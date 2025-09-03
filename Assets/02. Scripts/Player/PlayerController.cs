using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Áñ°Å¿î ³»ÀÏ¹è¿òÄ·ÇÁ 
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

        // Subscribe to the Pause input
        playerActions.Pause.performed += OnPausePressed;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid multiple calls
        playerActions.Pause.performed -= OnPausePressed;
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

    // Callback for the Escape key / Pause input
    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (UIManager.Instance != null)
            UIManager.Instance.SetPause();
        else
            Debug.LogWarning("UIManager instance not found!");
    }
}
