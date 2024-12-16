using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatsUI : MonoBehaviour
{
    [SerializeField] private UpgradeStatsButton buttonStatPrefab;

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform tr in transform)
        {
            Destroy(tr.gameObject);
        }

        Dictionary<Stat, StatData> stats = ResourceManager.Instance.PlayerStatsData.GetListStat();

        Debug.Log(stats.Keys.Count);

        foreach (Stat stat in stats.Keys)
        {
            UpgradeStatsButton statsButton = Instantiate(buttonStatPrefab, transform);
            statsButton.Setup(stats[stat]);
        }
    }
}
