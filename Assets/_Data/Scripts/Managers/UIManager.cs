using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>, IGameStateListener
{
    [SerializeField] private GameObject preparePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject phaseCompletePanel;
    [SerializeField] private GameObject trainingPanel;

    [SerializeField] private Popup popupCompleted;
    [SerializeField] private Popup popupGameOver;

    private List<GameObject> panels = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        panels.AddRange(new GameObject[] {
            preparePanel,
            gamePanel,
            phaseCompletePanel,
            trainingPanel,
            introPanel
        });

        ShowPanel(null);

    }

    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {

            case GameState.INTRO:
                ShowPanel(introPanel);
                break;
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

    public void ShowPopupCompleted() => popupCompleted.Show();
    public void ShowPopupGameOver() => popupGameOver.Show();


}
