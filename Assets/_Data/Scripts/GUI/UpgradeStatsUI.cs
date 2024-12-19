using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatsUI : MonoBehaviour, IPlayerStatsDepnedency
{
    [SerializeField] private UpgradeStatsButton buttonStatPrefab;

    private PlayerStats playerStats;

    private void Start()
    {
        CurrencyManager.onUpdated += CurrencyManager_onUpdated;
    }

    private void OnDestroy()
    {
        CurrencyManager.onUpdated -= CurrencyManager_onUpdated;
    }

    private void CurrencyManager_onUpdated()
    {
        UpdateVisual(playerStats);
    }

    private void UpdateVisual(PlayerStats playerStats)
    {

        StatData[] stats = ResourceManager.Instance.PlayerStatsData.stats;

        for (int i = 0; i < stats.Length; i++)
        {
            UpgradeStatsButton statsButton = transform.GetChild(i).GetComponent<UpgradeStatsButton>();
            statsButton.Setup(stats[i], playerStats);
        }
    }

    public void UpdateStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
        UpdateVisual(playerStats);
    }
}
