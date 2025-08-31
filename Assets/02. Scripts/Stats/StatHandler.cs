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
        InitializeStats();
        isAlive = baseStats.isAlive;
    }

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
}