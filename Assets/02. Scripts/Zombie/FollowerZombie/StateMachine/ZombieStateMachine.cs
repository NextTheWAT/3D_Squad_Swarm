using UnityEngine;


public class ZombieStateMachine : StateMachine
{
    public BaseZombie Zombie { get; }
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
    public ZombieChargeState ChargeState { get; }

    public ZombieStateMachine(BaseZombie zombie)
    {
        Zombie = zombie;

        IdleState = new ZombieIdleState(this);
        DeathState = new ZombieDeathState(this);

        if (zombie is FollowerZombie)
        {
            FollowState = new ZombieFollowState(this);
            ChasingState = new ZombieChasingState(this);
            AttackState = new ZombieAttackState(this);
            RiseState = new ZombieRiseState(this);
        }
        else if (zombie is ChargingZombie)
        {
            FollowState = new ZombieFollowState(this);
            ChasingState = new ZombieChasingState(this);
            AttackState = new ZombieAttackState(this);
            RiseState = new ZombieRiseState(this);
            ChargeState = new ZombieChargeState(this);
        }
    }

    public void SetDead()
    {
        IsDead = true;
        ChangeState(DeathState);
    }
    public float MovementSpeed => Zombie.Stats.GetStat(StatType.Speed) * MovementSpeedModifier;
    public float RotationDamping => Zombie.Stats.GetStat(StatType.RotationDamping) * RotationDampingModifier;
    public float DetectionRange => Zombie.Stats.GetStat(StatType.DetectRange);
    public float ChaseRange => Zombie.Stats.GetStat(StatType.ChaseRange);
    public float ChargeDistance => Zombie.Stats.GetStat(StatType.ChargeDistance);
}