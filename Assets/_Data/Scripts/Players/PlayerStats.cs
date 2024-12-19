using System;
using System.Collections.Generic;
using System.Linq;
using Saving;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IWantToBeSaved
{
    const string PlayerStatsKey = "PlayerStatsKey";

    public Dictionary<Stat, int> Levels { get; private set; } = new Dictionary<Stat, int>
    {
        {Stat.STAMINA,0},
        {Stat.SPEED,0},
        {Stat.INCOME,0},
    };

    private void Start()
    {
        UpdatePlayerStats();
    }



    public float GetValueStat(Stat stat) => ResourceManager.Instance.PlayerStatsData.GetValue(stat, Levels[stat]);
    public float GetCostStatNextLV(Stat stat) => ResourceManager.Instance.PlayerStatsData.GetCost(stat, Levels[stat] + 1);

    public void UpgradeStat(Stat stat)
    {
        if (Levels.ContainsKey(stat))
        {
            Levels[stat]++;
        }
        else
        {
            Debug.LogWarning($"The key {stat} has not been found");
        }

        Save();
        UpdatePlayerStats();


    }

    private void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsDepnedency> playerStatsDepnedencies = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IPlayerStatsDepnedency>();

        foreach (IPlayerStatsDepnedency depnedency in playerStatsDepnedencies)
        {
            depnedency.UpdateStats(this);
        }
    }

    public void Load()
    {
        if (SaveSystem.TryLoad(this, PlayerStatsKey, out object playerStatsData))
        {
            PlayerStatData data = (PlayerStatData)playerStatsData;
            Levels = new Dictionary<Stat, int>
            {
                {Stat.STAMINA,data.StaminaLv},
                {Stat.SPEED,data.SpeedLv},
                {Stat.INCOME,data.IncomeLv},
            };
        }
        else
        {
            Levels = new Dictionary<Stat, int>
            {
                {Stat.STAMINA,0},
                {Stat.SPEED,0},
                {Stat.INCOME,0},
            };

            Save();
        }
    }

    public void Save()
    {
        PlayerStatData data = new PlayerStatData
        {
            StaminaLv = Levels[Stat.STAMINA],
            SpeedLv = Levels[Stat.SPEED],
            IncomeLv = Levels[Stat.INCOME]
        };

        SaveSystem.Save(this, PlayerStatsKey, data);
    }

    [Serializable]
    public class PlayerStatData
    {
        public int StaminaLv;
        public int SpeedLv;
        public int IncomeLv;
    }

}
