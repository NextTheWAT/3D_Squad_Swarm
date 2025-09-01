using UnityEngine;
using UnityEngine.AI;

public enum NPCType
{
    Civilian,   // 일반 사람
    Hunter,     // 사냥꾼 (좀비 추적)
    VIP         // 보호해야 하는 사람
}
public class NPC : MonoBehaviour, IDamageable
{

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    public StatHandler Stats { get; private set; }

    private NPCStateMachine stateMachine;
    public ForceReceiver ForceReceiver { get; private set; }


    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        Stats= GetComponent<StatHandler>();

        stateMachine = new NPCStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
        ForceReceiver = GetComponent<ForceReceiver>();
    }

    private void Update()
    {
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

}
   
        
