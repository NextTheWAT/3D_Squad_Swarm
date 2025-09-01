using UnityEngine;

public class ZombieStateMachine : StateMachine
{
    public FollowerZombie Zombie { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; }

    public GameObject Target { get; private set; }
    public ZombieIdleState IdleState { get; }
    public ZombieFollowState FollowState { get; }
    public ZombieChasingState ChasingState { get; }
    public ZombieAttackState AttackState { get; }

    public ZombieStateMachine(FollowerZombie Zombie)
    {
        this.Zombie = Zombie;

        IdleState = new ZombieIdleState(this);
        FollowState = new ZombieFollowState(this);
        ChasingState = new ZombieChasingState(this);
        AttackState = new ZombieAttackState(this);
    }
    public float MovementSpeed => Zombie.Stats.GetStat(StatType.Speed) * MovementSpeedModifier;
    public float RotationDamping => Zombie.Stats.GetStat(StatType.RotationDamping);
    public float DetectionRange => Zombie.Stats.GetStat(StatType.DetectRange);
}