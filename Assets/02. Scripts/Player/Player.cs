using UnityEngine;

public class Player : MonoBehaviour
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
}