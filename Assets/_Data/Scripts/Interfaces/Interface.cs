public interface IGameStateListener
{
    void GameStateChangeCallback(GameState gameState);
}

public interface IPlayerStatsDepnedency
{
    void UpdateStats(PlayerStats playerStats);
}
