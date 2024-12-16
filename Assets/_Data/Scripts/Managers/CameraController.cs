using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraController : MonoBehaviour, IGameStateListener
{

    [SerializeField] private PlayableDirector introSequence;
    [SerializeField] private PlayableDirector outtroSequence;

    [SerializeField] private GameObject camFollow;
    [SerializeField] private GameObject camCM1;
    [SerializeField] private GameObject camCM2;

    private List<GameObject> panels = new List<GameObject>();



    private void Awake()
    {
        panels.AddRange(new GameObject[] {
            camFollow,
            camCM1,
            camCM2,
        });

        introSequence.stopped += OnIntroCompleted;
        outtroSequence.stopped += OnOuttroCompleted;
    }



    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.TRAINING:
                ShowCam(camCM1);
                break;

            case GameState.INTRO:
                ShowCam(camFollow);
                introSequence.Play();
                break;

            case GameState.PHASECOMPLETE:
                ShowCam(camCM1);
                break;

            case GameState.OUTTRO:
                ShowCam(camCM2);
                outtroSequence.Play();
                break;
        }
    }

    private void ShowCam(GameObject cam)
    {
        foreach (GameObject item in panels)
        {
            item.SetActive(item == cam);
        }
    }

    private void OnIntroCompleted(PlayableDirector director)
    {
        GameManager.Instance.SetGameState(GameState.PREPARE);
    }

    private void OnOuttroCompleted(PlayableDirector director)
    {
        GameManager.Instance.SetGameState(GameState.STAGECOMPLETE);
    }


}
