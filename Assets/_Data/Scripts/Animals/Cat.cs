using System;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private float interactTime = 1;

    private float currentValue;
    private bool isCaptured;

    public bool IsCaptured()
    {
        return isCaptured;
    }

    public void SetProgression(float value)
    {
        progressBar.gameObject.SetActive(true);
        currentValue += value;
        progressBar.fillAmount = currentValue / interactTime;

        if (progressBar.fillAmount >= 1)
        {
            Debug.Log("done");
            isCaptured = true;
            ResetProgression();
        }
    }

    public void ResetProgression()
    {
        progressBar.gameObject.SetActive(false);
        currentValue = 0;
        progressBar.fillAmount = 0;
    }
}
