// NPCStateMachine.cs
using UnityEngine;

public class NPCStateMachine : StateMachine
{
    public NPC Npc { get; }

    public float MovementSpeedModifier { get; set; } = 1f;
    public float RotationDampingModifier { get; set; } = 1f;
    public bool IsDead { get; private set; } = false;

    public GameObject Target { get; private set; }
    public NPCIdleState IdleState { get; }
    public NPCChaseState ChaseState { get; }
    public NPCAttackState AttackState { get; }
    public NPCFleeState FleeState { get; }
    public NPCDeathState DeathState { get; }

    private const string PlayerTag = "Player";
    private const string ZombieTag = "Zombie";

    public NPCStateMachine(NPC npc)
    {
        this.Npc = npc;
        Target = GameObject.FindGameObjectWithTag(PlayerTag);

        IdleState = new NPCIdleState(this);
        ChaseState = new NPCChaseState(this);
        AttackState = new NPCAttackState(this);
        FleeState = new NPCFleeState(this);
        DeathState = new NPCDeathState(this);
    }

    public void SetDead()
    {
        IsDead = true;
        ChangeState(DeathState);
    }

    // 매 프레임 호출: 플레이어/좀비 중 가장 가까운 오브젝트를 Target으로
    public void RefreshTargetEveryFrame()
    {
        Vector3 origin = Npc.transform.position;

        GameObject best = null;
        float bestSqr = float.PositiveInfinity;

        // 1) Player 1명
        var player = GameObject.FindWithTag(PlayerTag);
        if (player != null)
        {
            float d = (player.transform.position - origin).sqrMagnitude;
            if (d < bestSqr) { best = player; bestSqr = d; }
        }

        // 2) Zombies 다수
        var zombies = GameObject.FindGameObjectsWithTag(ZombieTag);
        for (int i = 0; i < zombies.Length; i++)
        {
            var z = zombies[i];
            if (z == null || z == Npc.gameObject) continue; // 자기 자신 제외
            float d = (z.transform.position - origin).sqrMagnitude;
            if (d < bestSqr) { best = z; bestSqr = d; }
        }

        if (best != null) Target = best; // 하나라도 찾았을 때만 갱신
    }
}
