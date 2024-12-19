using System;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrow : MonoBehaviour, IGameStateListener
{
    [SerializeField] private GameObject arrow;

    private bool isActive;

    private void Start()
    {
        Hide();
    }
    private void Update()
    {
        if (!isActive) return;

        if (LevelManager.Instance.RemainCatList().Count > 0)
        {
            ActiveNavigationArrow(LevelManager.Instance.RemainCatList());
        }

    }

    private void ActiveNavigationArrow(List<Cat> list)
    {
        int index = GetIndexCatNearest(list);
        if (index < 0)
        {
            Hide();
        }
        else
        {
            SetDir(list[index].transform.position);
        }
    }


    private int GetIndexCatNearest(List<Cat> list)
    {
        float maxDis = 1000f;
        int indexCatNearest = -1;

        for (int i = 0; i < list.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, list[i].transform.position);
            if (distance < maxDis)
            {
                maxDis = distance;
                indexCatNearest = i;
            }
        }

        return indexCatNearest;
    }


    public void SetDir(Vector3 target)
    {
        arrow.SetActive(true);
        Vector3 dir = target - transform.position;
        arrow.transform.forward = dir.normalized;
    }

    public void Hide()
    {
        arrow.SetActive(false);
    }

    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {

            case GameState.GAME:
                isActive = true;
                break;

            case GameState.PHASECOMPLETE:
                isActive = false;
                Hide();
                break;
        }
    }
}
