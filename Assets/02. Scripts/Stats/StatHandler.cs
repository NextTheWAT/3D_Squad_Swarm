using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Header("Base Stats (from ScriptableObject)")]
    public ScriptableStats baseStats;

    private Dictionary<StatType, float> statValues = new Dictionary<StatType, float>();
    public bool isAlive = true;

    private void Awake()
    {
        // InitializeStats();  // ← 이 줄은 제거 (중복)
        ApplyFrom(baseStats, clearBefore: true);        // baseStats가 있으면 적용
        isAlive = baseStats != null ? baseStats.isAlive : true;  // 널가드
    }

    //필요한지 모르겠습니다..ㅎㅎ 저는 필요없다고 판단됩니다!
    private void InitializeStats()
    {
        statValues.Clear();
        foreach (var entry in baseStats.stats)
        {
            statValues[entry.statType] = entry.baseValue;
        }
    }

    public float GetStat(StatType type)
    {
        return statValues.ContainsKey(type) ? statValues[type] : 0f;
    }

    public void BoostStat(StatType type, float multtplier, float duration)
    {
        StartCoroutine(BoostRoutine(type, multtplier, duration));
    }

    private IEnumerator BoostRoutine(StatType type, float multiplier, float duration)
    {
        if (!statValues.ContainsKey(type))
        {
            yield break;
        }

        statValues[type] *= multiplier;
        yield return new WaitForSeconds(duration);
        statValues[type] /= multiplier;
    }

    //스테이지 매니저에서 사용 현재 스탯 초기화 용도
    public void ApplyFrom(ScriptableStats s, bool clearBefore = true)
    {
        if (s == null) return;
        if (clearBefore) statValues.Clear();
        foreach (var entry in s.stats)
            statValues[entry.statType] = entry.baseValue;
    }
}