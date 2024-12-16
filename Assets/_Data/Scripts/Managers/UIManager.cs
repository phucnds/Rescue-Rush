using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IGameStateListener
{
    [SerializeField] private GameObject preparePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject phaseCompletePanel;
    [SerializeField] private GameObject trainingPanel;

    private List<GameObject> panels = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        panels.AddRange(new GameObject[] {
            preparePanel,
            gamePanel,
            phaseCompletePanel,
            trainingPanel
        });
    }

    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PREPARE:
                ShowPanel(preparePanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;

            case GameState.PHASECOMPLETE:
                ShowPanel(phaseCompletePanel);
                break;

            case GameState.TRAINING:
                ShowPanel(trainingPanel);
                break;
        }
    }

    private void ShowPanel(GameObject panel)
    {
        foreach (GameObject item in panels)
        {
            item.SetActive(item == panel);
        }
    }
}
