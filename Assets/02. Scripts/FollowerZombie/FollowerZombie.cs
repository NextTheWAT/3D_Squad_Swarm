using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FollowerZombie : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] public PlayerAnimationData animationData;

    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public StatHandler Stats { get; private set; }

    public Transform PlayerTarget { get; private set; }
    public Transform EnemyTarget { get; set; }

    public ZombieStateMachine stateMachine;

    [Header("Follow Settings")]
    [SerializeField] public float followRange = 2f;   // Stay near player

    private void Awake()
    {
        animationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Stats = GetComponent<StatHandler>();

        stateMachine = new ZombieStateMachine(this);
        stateMachine.ChangeState(stateMachine.RiseState);
    }

    private void Start()
    {
        PlayerTarget = GameObject.FindWithTag("Player").transform;
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

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    // ---------------------------
    // IDamageable & Combat Logic
    // ---------------------------
    public void OnTakeDamage(bool isAlive)
    {
        Stats.isAlive = false;
    }
    public void OnAttackAnimationComplete()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    public void OnRiseComplete()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    // Trigger → Attack logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(RotateTowardEnemy(other.transform));
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    private IEnumerator RotateTowardEnemy(Transform enemy)
    {
        float elapsed = 0f;
        float duration = 0.2f; 

        Quaternion startRotation = transform.rotation;
        Vector3 direction = enemy.position - transform.position;
        direction.y = 0f; // Keep flat
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && EnemyTarget == other.transform)
        {
            EnemyTarget = null;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public void OnAttackHit()
    {
        Debug.Log("Attack animation event triggered");

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
        if (stateMachine.Zombie.Agent != null)
            stateMachine.Zombie.Agent.enabled = false;

        // Destroy or deactivate zombie
        GameObject.Destroy(stateMachine.Zombie.gameObject); // Or use SetActive(false)
    }
}
