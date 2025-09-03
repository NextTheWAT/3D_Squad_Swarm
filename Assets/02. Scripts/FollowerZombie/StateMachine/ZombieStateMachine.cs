using UnityEngine;

public class ZombieStateMachine : StateMachine
{
    public FollowerZombie Zombie { get; }
    public bool IsDead { get; private set; } = false;

    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; }
    public float RotationDampingModifier { get; set; }

    public GameObject Target { get; private set; }
    public ZombieIdleState IdleState { get; }
    public ZombieFollowState FollowState { get; }
    public ZombieChasingState ChasingState { get; }
    public ZombieAttackState AttackState { get; }
    public ZombieDeathState DeathState { get; }
    public ZombieRiseState RiseState { get; }

    public ZombieStateMachine(FollowerZombie Zombie)
    {
        this.Zombie = Zombie;
        MovementSpeedModifier = 1f;

        IdleState = new ZombieIdleState(this);
        FollowState = new ZombieFollowState(this);
        ChasingState = new ZombieChasingState(this);
        AttackState = new ZombieAttackState(this);
        DeathState = new ZombieDeathState(this);
        RiseState = new ZombieRiseState(this);
    }

    public void SetDead()
    {
        IsDead = true;
        ChangeState(DeathState);
    }
    public float MovementSpeed => Zombie.Stats.GetStat(StatType.Speed) * MovementSpeedModifier;
    public float RotationDamping => Zombie.Stats.GetStat(StatType.RotationDamping) * RotationDampingModifier;
    public float DetectionRange => Zombie.Stats.GetStat(StatType.DetectRange);
}