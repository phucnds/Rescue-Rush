using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        SetGameState(GameState.INTRO);
    }

    public void SetGameState(GameState state)
    {
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
        {
            gameStateListener.GameStateChangeCallback(state);
        }
    }

    public void StartGame() => SetGameState(GameState.GAME);

}
