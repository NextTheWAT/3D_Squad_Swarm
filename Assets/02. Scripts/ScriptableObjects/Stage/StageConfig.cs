using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    [Header("Scene")]
    public string sceneName; // Build Settings에 등록된 씬 이름

    [System.Serializable]
    public class UnitEntry
    {
        public string unitId;           // 유닛 식별자(= GameObject.tag)
        public ScriptableStats stats;   // 이 유닛이 이 스테이지에서 쓸 스탯
    }

    [Header("Unit → Stats")]
    public List<UnitEntry> unitStats = new();

    public ScriptableStats GetStatsFor(string unitId)
    {
        if (string.IsNullOrEmpty(unitId)) return null;
        for (int i = 0; i < unitStats.Count; i++)
        {
            var e = unitStats[i];
            if (e != null && e.unitId == unitId) return e.stats;
        }
        return null;
    }
}
