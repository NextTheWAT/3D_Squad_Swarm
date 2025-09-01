using UnityEngine;
using UnityEngine.AI;

public class npc : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private NPCStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();

        //stateMachine = new NPCStateMachine(this);
        //stateMachine.ChangeState(stateMachine.IdleState);
    }



}
