using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Dictionary<Stat, int> Levels { get; private set; } = new Dictionary<Stat, int>
    {
        {Stat.STAMINA,0},
        {Stat.SPEED,0},
        {Stat.INCOME,0},
    };

    public float GetValueStat(Stat stat) => ResourceManager.Instance.PlayerStatsData.GetValue(stat, Levels[stat]);
    public float GetCostStatNextLV(Stat stat) => ResourceManager.Instance.PlayerStatsData.GetCost(stat, Levels[stat] + 1);
    public void UpgradeStat(Stat stat) => Levels[stat]++;

}
