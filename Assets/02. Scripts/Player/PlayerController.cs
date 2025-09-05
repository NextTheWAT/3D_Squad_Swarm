using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInputs { get; private set; }
    public PlayerInput.PlayerMovementActions playerActions { get; private set; }
    public Player player { get; private set; }

    private AudioSource attackSoundEffect;

    private void Awake()
    {
        playerInputs = new PlayerInput();
        playerActions = playerInputs.PlayerMovement;
        player = GetComponent<Player>();

        attackSoundEffect = GetComponent<AudioSource>();
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
            attackSoundEffect.PlayOneShot(attackSoundEffect.clip);

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.OnTakeDamage(false);
            }
            StartCoroutine(RotateTowardEnemy(other.transform));
            player.stateMachine.ChangeState(player.stateMachine.AttackState);
        }
    }

    private IEnumerator RotateTowardEnemy(Transform enemy)
    {
        float elapsed = 0f;
        float duration = 0.2f; 

        Quaternion startRotation = player.transform.rotation;
        Vector3 direction = enemy.position - player.transform.position;
        direction.y = 0f; // Keep flat
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            yield return null;
        }

        // Ensure it ends exactly at target rotation
        player.transform.rotation = targetRotation;
    }



    // Callback for the Escape key / Pause input
    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.SetPause();
            GameManager.Instance.OnPause(true);
        }
        else
            Debug.LogWarning("UIManager instance not found!");
    }
}
