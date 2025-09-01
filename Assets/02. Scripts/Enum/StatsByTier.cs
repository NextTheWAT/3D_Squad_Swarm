using UnityEditor;

[System.Serializable]
public class StatsByTier
{
    public UnitStats stage1;   // StageTier.Stage1
    public UnitStats stage2;   // StageTier.Stage2
    public UnitStats stage3;   // StageTier.Stage3

    public UnitStats Get(StageTier tier)
    {
        switch (tier)
        {
            case StageTier.Stage1: return stage1;
            case StageTier.Stage2: return stage2;
            case StageTier.Stage3: return stage3;
            default: return stage1;
        }
    }
}
