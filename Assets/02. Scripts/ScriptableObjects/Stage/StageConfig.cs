using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    [Header("Scene")]
    public string sceneName; // Build Settings�� ��ϵ� �� �̸�

    [System.Serializable]
    public class UnitEntry
    {
        public string unitId;           // ���� �ĺ���(= GameObject.tag)
        public ScriptableStats stats;   // �� ������ �� ������������ �� ����
    }

    [Header("Unit �� Stats")]
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
