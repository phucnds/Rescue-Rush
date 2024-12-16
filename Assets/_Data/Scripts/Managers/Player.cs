using UnityEngine;

public class Player : Singleton<Player>
{
    private PlayerStats playerStats;

    protected override void Awake()
    {
        base.Awake();

        playerStats = GetComponent<PlayerStats>();
    }

    public PlayerStats PlayerStats { get { return playerStats; } }
}
