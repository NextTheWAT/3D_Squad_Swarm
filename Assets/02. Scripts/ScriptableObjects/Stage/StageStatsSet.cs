using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageStatsSet")]
public class StageStatsSet : ScriptableObject
{
    [Header("Human")]
    public StatsByTier human;

    [Header("VIP Human")]
    public StatsByTier vipHuman;

    [Header("Hunter")]
    public StatsByTier hunter;

    public UnitStats Get(CharacterTier kind, StageTier tier)
    {
        switch (kind)
        {
            case CharacterTier.Human: return human.Get(tier);
            case CharacterTier.VipHuman: return vipHuman.Get(tier);
            case CharacterTier.Hunter: return hunter.Get(tier);
        }
        return null;
    }
}
