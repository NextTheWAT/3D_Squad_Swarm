using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    [Header("Stage Meta")]
    public int stageId = 1;
    public string stageName = "Stage 1";
    public float timeLimitSec = 120f;

    [System.Serializable]
    public class UnitEntry
    {
        public string unitId;          // 예: "Human", "VIP", "Hunter" (또는 Tag 이름)
        public ScriptableStats stats;  // 이 유닛이 이 스테이지에서 쓸 수치 세트
    }

    [Header("UnitId → ScriptableStats")]
    public List<UnitEntry> unitStats = new List<UnitEntry>();

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
