using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType  // Type of stats
{
    JumpPower,
    Speed,
    AttackSpeed,
    DetectRange,
    RotationDamping,
    ChaseRange
}

[CreateAssetMenu(fileName = "ScriptableStats", menuName = "Stats/ScriptableStats")]
public class ScriptableStats : ScriptableObject
{
    public bool isAlive = true; //Determines if the player is dead or alive
    public List<StatEntry> stats = new List<StatEntry>(); //Gets the stats into a list

    [System.Serializable]
    public class StatEntry
    {
        public StatType statType;
        public float baseValue;
    }

    public float GetBaseValue(StatType type) //Used when needed to get a certain type of stat
    {
        StatEntry entry = null;
        foreach (var s in stats)
        {
            if (s.statType == type)
            {
                entry = s;
                break;
            }
        }
        return entry != null ? entry.baseValue : 0f;
    }
}