using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    [Header("StageInfo")]
    public int stageId;
    public string stageName;
    public Vector2 mapSize;
    public float timeLimitSec = 120f;
    [Range(0, 100)] public float targetInfectionPercent = 100f;

    [Header("Stage Tier")]
    public StageTier stageTier = StageTier.Stage1;   // ★ 현재 스테이지 구분

    [Header("Reward")]
    public float humanReward;
    public float hunterReward;

    [Header("Human Spawn")]
    public int humanInit;
    public int humanMax;
    public float humanRegenCooldown;

    [Header("VIP Spawn")]
    public int vipInit;
    public int vipMax;
    public float vipRegenCooldown;

    [Header("Hunter Spawn")]
    public int hunterMaxCommon;
    public float hunterRegenCooldown;
    public float hunterStartThreshold = 30f;
    public int hunterInit;
    public int hunterMaxStage3;

    [Header("Stats Source (Separate SO)")]
    public StageStatsSet statsSet;                   // ★ 스테이지별 능력치 세트 참조

    // --- 편의 접근자 ---
    public UnitStats GetStats(CharacterTier kind)
    {
        if (statsSet == null) return null;
        return statsSet.Get(kind, stageTier);
    }

    public UnitStats HumanStats => GetStats(CharacterTier.Human);
    public UnitStats VipStats => GetStats(CharacterTier.VipHuman);
    public UnitStats HunterStats => GetStats(CharacterTier.Hunter);
}
