using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public StatHandler Stats { get; private set; }

    public ForceReceiver ForceReceiver { get; private set; }

    public PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        Stats = GetComponent<StatHandler>();
        stateMachine = new PlayerStateMachine(this);

        stateMachine.ChangeState(stateMachine.IdleState);

        ForceReceiver = GetComponent<ForceReceiver>();
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {

        if (Stats.isAlive == false && !stateMachine.IsDead)
        {
            stateMachine.SetDead();
        }

        if (stateMachine.IsDead)
            return; // Stop all other logic

        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    public void OnAttackAnimationComplete()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    public void OnTakeDamage(bool isAlive)
    {
        Stats.isAlive = false;
    }

    public void OnAttackHit()
    {
        Debug.Log("Attack");
        float attackRange = 1.5f;
        float attackRadius = 0.5f;

        Vector3 attackOrigin = transform.position + transform.forward * attackRange;

        Collider[] hits = Physics.OverlapSphere(attackOrigin, attackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                if (hit.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.OnTakeDamage(false);
                }
            }
        }
    }
    public void OnDeathAnimationComplete()
    {
        // Disable NavMeshAgent
        if (stateMachine.Player.Controller != null)
        {
            stateMachine.Player.Controller.enabled = false;
        }
    }
}