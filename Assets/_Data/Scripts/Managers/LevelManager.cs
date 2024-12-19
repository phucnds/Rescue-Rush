using System;
using System.Collections.Generic;
using Saving;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>, IGameStateListener, IWantToBeSaved
{
    [Header("Level")]

    const string GameLevelKey = "GameLevel";

    [SerializeField] private Level levelData;
    [SerializeField] private int GameLevel = 0;
    [SerializeField] private LevelData[] ListLevelData;

    [Space(5)]
    [NaughtyAttributes.HorizontalLine]

    [Header("Player")]
    [SerializeField] private PlayerMovement player;

    private List<Cat> remainList = new List<Cat>();
    private int remainAmount = 0;

    public Action<List<Cat>> OnSetPositionCat;

    public void OnRescueComplete(Cat cat)
    {
        remainAmount--;

        if (remainAmount <= 0)
        {
            GameManager.Instance.SetGameState(GameState.PHASECOMPLETE);
        }

        remainList.Remove(cat);
        // Debug.Log("rescue: " + remainAmount);
    }

    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.INTRO:
                Intialize();
                break;

            case GameState.GAME:
                StartGame();
                OnSetPositionCat?.Invoke(levelData.GetCats());
                break;

            case GameState.PHASECOMPLETE:
                PlayerGotoGoal();
                break;

            case GameState.GAMEOVER:
                StopTsunami();
                UIManager.Instance.ShowPopupGameOver();
                break;

            case GameState.STAGECOMPLETE:
                StopTsunami();
                UIManager.Instance.ShowPopupCompleted();
                SetGameLevel(GameLevel + 1);
                break;
        }
    }

    private void Intialize()
    {
        Debug.Log(GameLevel);

        LevelData data = ListLevelData[GameLevel];
        levelData.SetupLevel(data.AmountObstacle, data.TsunamiWaveSpeed);
        levelData.BuildLevel();

        remainAmount = levelData.GetCats().Count;
        remainList = levelData.GetCats();
    }

    private void StopTsunami() => levelData.StopTsunamiWave();

    public TsunamiWave GetTsunami() => levelData.TsunamiWave;

    private void StartGame() => levelData.StartTsunamiWave();

    private void PlayerGotoGoal()
    {
        player.Follow(levelData.EndPointPhase1, () =>
        {
            GameManager.Instance.SetGameState(GameState.OUTTRO);
            player.Follow(levelData.EndPointPhase2, () =>
            {

            });
        });
    }

    public List<Cat> RemainCatList()
    {
        return remainList;
    }

    public List<Cat> LstCatThisLevel()
    {
        return levelData.GetCats();
    }

    public float GetLengthP1() => levelData.GetLengthP1();

    public void Load()
    {
        if (SaveSystem.TryLoad(this, GameLevelKey, out object gameLvValue))
        {
            SetGameLevel((int)gameLvValue);
        }
        else
        {
            SetGameLevel(0);
        }
    }

    public void SetGameLevel(int lv)
    {
        GameLevel = lv;
        GameLevel = Mathf.Clamp(GameLevel, 0, ListLevelData.Length - 1);
        Save();
    }

    public void Save()
    {
        SaveSystem.Save(this, GameLevelKey, GameLevel);
    }
}
