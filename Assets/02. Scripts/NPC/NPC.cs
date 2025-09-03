using UnityEngine;
using UnityEngine.AI;

public enum NPCType
{
    Civilian,   // �Ϲ� ���
    Hunter,     // ��ɲ� (���� ����)
    VIP         // ��ȣ�ؾ� �ϴ� ���
}
public class NPC : MonoBehaviour, IDamageable
{
    public NPCType npcType;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    [Header("Zombie")]
    public GameObject zombiePrefab;

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }

    public StatHandler Stats { get; private set; }

    private NPCStateMachine stateMachine;
    //public ForceReceiver ForceReceiver { get; private set; }
    public NavMeshAgent agent;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Stats= GetComponent<StatHandler>();

        stateMachine = new NPCStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
        //ForceReceiver = GetComponent<ForceReceiver>();
       
    }

    private void Update()
    {
        if (Stats.isAlive == false && !stateMachine.IsDead)
        {
            stateMachine.SetDead();
        }

        if (stateMachine.IsDead)
            return; // Stop all other logic

        stateMachine.RefreshTargetEveryFrame(); //Ÿ�� ������Ʈ
        stateMachine.Update();

    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void OnTakeDamage(bool isAlive)
    {
        Stats.isAlive = false;
    }

    public void OnShootEvent()
    {
        stateMachine.AttackState.Shoot();
    }

    public void ChangeToZombie()
    {
        GameObject zombie = GameObject.Instantiate(zombiePrefab, stateMachine.Npc.transform.position, stateMachine.Npc.transform.transform.rotation);
        Destroy(gameObject);

        FollowerZombie followerZombie = zombie.GetComponent<FollowerZombie>();
        if (followerZombie != null && ZombieManager.Instance != null)
        {
            ZombieManager.Instance.RegisterZombie(followerZombie);
        }
    }
    public void Die()
    {
        StageManager.Instance?.OnEnemyKilled(this);
        // ���ϸ� ���⼭ Destroy(gameObject)���� ó��
    }
}
   
        
