using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class BaseZombie : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] public PlayerAnimationData animationData;

    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public StatHandler Stats { get; private set; }

    public Transform PlayerTarget { get; protected set; }
    public Transform EnemyTarget { get; set; }

    public ZombieStateMachine stateMachine;

    [Header("Follow Settings")]
    [SerializeField] public float followRange = 2f;

    protected virtual void Awake()
    {
        animationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Stats = GetComponent<StatHandler>();

        stateMachine = new ZombieStateMachine(this);
        stateMachine.ChangeState(stateMachine.RiseState);
    }

    protected virtual void Start()
    {
        PlayerTarget = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (Stats.isAlive == false && !stateMachine.IsDead)
            stateMachine.SetDead();

        if (stateMachine.IsDead)
            return;

        stateMachine.HandleInput();
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    // ---------------------------
    // Shared logic
    // ---------------------------
    public Transform FindClosestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, stateMachine.DetectionRange);
        float minDist = Mathf.Infinity;
        Transform closest = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = hit.transform;
                }
            }
        }
        return closest;
    }

    // ---------------------------
    // IDamageable & Combat Logic
    // ---------------------------
    public virtual void OnTakeDamage(bool isAlive)
    {
        Stats.isAlive = false;
    }

    public virtual void OnAttackAnimationComplete()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    public virtual void OnRiseComplete()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    public virtual void OnAttackHit()
    {
        Debug.Log("Attack animation event triggered");

        float attackRange = 1.5f;
        float attackRadius = 0.5f;

        Vector3 attackOrigin = transform.position + transform.forward * attackRange;
        Collider[] hits = Physics.OverlapSphere(attackOrigin, attackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") && hit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.OnTakeDamage(false);
            }
        }
    }

    public virtual void OnDeathAnimationComplete()
    {
        if (stateMachine.Zombie.Agent != null)
            stateMachine.Zombie.Agent.enabled = false;

        Destroy(stateMachine.Zombie.gameObject);
    }

    // ---------------------------
    // Triggers
    // ---------------------------
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(RotateTowardEnemy(other.transform));
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && EnemyTarget == other.transform)
        {
            EnemyTarget = null;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    protected IEnumerator RotateTowardEnemy(Transform enemy)
    {
        float elapsed = 0f;
        float duration = 0.2f;

        Quaternion startRotation = transform.rotation;
        Vector3 direction = enemy.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
