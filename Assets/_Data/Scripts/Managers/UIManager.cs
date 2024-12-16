using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] private GameObject preparePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject phaseCompletePanel;

    private List<GameObject> panels = new List<GameObject>();

    private void Awake()
    {
        panels.AddRange(new GameObject[] {
            preparePanel,
            gamePanel,
            phaseCompletePanel
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
