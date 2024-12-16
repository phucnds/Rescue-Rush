using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager : Singleton<LevelManager>, IGameStateListener
{


    [Header("Roads")]
    [SerializeField] private GameObject roads;
    [field: SerializeField] public Vector3 endPointPhase1 { get; private set; } = new Vector3(50, 0, 400);
    [field: SerializeField] public Vector3 endPointPhase2 { get; private set; } = new Vector3(50, 0, 1400);
    [Space(5)]
    [NaughtyAttributes.HorizontalLine]

    [Header("Tsunami")]
    [SerializeField] private TsunamiWave tsunamiWave;
    [SerializeField] private Vector3 tsunaminPos = new Vector3(50, 0, -150);
    [SerializeField] private float moveSpeed = 10;

    private TsunamiWave tsunami;
    [Space(5)]
    [NaughtyAttributes.HorizontalLine]

    [Header("Animals")]
    [SerializeField] private Cat catPrefab;
    [SerializeField] private Vector3[] arrPos;
    [SerializeField] private int remainCat = 0;
    private List<Cat> lstCatThisLevel = new List<Cat>();

    [Space(5)]
    [NaughtyAttributes.HorizontalLine]

    [Header("Player")]
    [SerializeField] private PlayerController player;
    [Space(5)]
    [NaughtyAttributes.HorizontalLine]

    [SerializeField] private Transform environmentParent;
    [SerializeField] private bool DEBUG;

    public Action<List<Cat>> OnSetPositionCat;

    private void Start()
    {
        Intialize();


    }

    public void OnRescueComplete()
    {
        remainCat--;
        if (remainCat <= 0)
        {
            GameManager.Instance.SetGameState(GameState.PHASECOMPLETE);
        }

        Debug.Log("rescue: " + remainCat);
    }

    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                StartGame();
                OnSetPositionCat?.Invoke(lstCatThisLevel);
                break;

            case GameState.PHASECOMPLETE:
                PlayerGotoGoal();
                break;

            case GameState.STAGECOMPLETE:
                StopTsunami();
                break;
        }
    }

    private void Intialize()
    {
        CreateRoads();

        CreateTsunami();

        CreateListCat();
    }

    private void CreateRoads()
    {
        Instantiate(roads, environmentParent);
    }

    private void CreateTsunami()
    {
        tsunami = Instantiate(tsunamiWave, tsunaminPos, Quaternion.identity, environmentParent);
        tsunami.SetMoveSpeed(0);
    }

    private void StopTsunami()
    {
        tsunami.SetMoveSpeed(0);
    }

    public TsunamiWave GetTsunami()
    {
        return tsunami;
    }

    private void CreateListCat()
    {
        remainCat = 0;

        foreach (Vector3 pos in arrPos)
        {
            Cat cat = Instantiate(catPrefab, pos, Quaternion.identity, environmentParent);
            lstCatThisLevel.Add(cat);
            remainCat++;
        }
    }



    private void StartGame()
    {
        tsunami.SetMoveSpeed(moveSpeed);

        if (DEBUG)
        {
            player.StarMovement(arrPos);
        }
    }



    private void PlayerGotoGoal()
    {


        player.MoveTo(endPointPhase1, () =>
        {
            GameManager.Instance.SetGameState(GameState.OUTTRO);

            player.MoveTo(endPointPhase2, () =>
            {

            });
        });
    }



}
