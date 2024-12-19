using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "PlayerStatsData", order = 0)]
public class PlayerStatsData : ScriptableObject
{
    [field: SerializeField] public StatData[] stats { get; private set; }

    private Dictionary<Stat, StatData> lookupStats;

    private void BuildLoopkup()
    {
        if (lookupStats != null) return;

        lookupStats = new Dictionary<Stat, StatData>();

        foreach (StatData sd in stats)
        {
            lookupStats[sd.Stat] = sd;
        }
    }

    public float GetValue(Stat stat, int level)
    {
        BuildLoopkup();
        // Debug.Log($"{lookupStats[stat].InitValue} + {lookupStats[stat].ValueIncreasePerLevel} * {level} = {lookupStats[stat].InitValue + lookupStats[stat].ValueIncreasePerLevel * level}");
        return lookupStats[stat].InitValue + lookupStats[stat].ValueIncreasePerLevel * level;
    }

    public float GetCost(Stat stat, int level)
    {
        BuildLoopkup();

        return lookupStats[stat].InitCost + lookupStats[stat].CostIncreasePerLevel * level;
    }

    public Dictionary<Stat, StatData> GetListStat()
    {
        BuildLoopkup();

        return lookupStats;
    }
}

public enum Stat
{
    STAMINA,
    SPEED,
    INCOME
}

[Serializable]
public class StatData
{
    public Stat Stat;
    public Sprite Icon;
    public float InitValue;
    public float InitCost;
    public float ValueIncreasePerLevel;
    public float CostIncreasePerLevel;
}