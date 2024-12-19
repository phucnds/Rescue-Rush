using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainSystem : MonoBehaviour
{
    [SerializeField] private Button touchZone;
    [SerializeField] private Button btnAuto;
    [SerializeField] private TextMeshProUGUI txtAuto;
    [SerializeField] private PlayerStamina playerStamina;

    [SerializeField] private Button btnRun;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        touchZone.onClick.AddListener(() =>
        {
            playerStamina.Tap();
            SceneLoading.Instance.ToggleTutFocus(false);
        });

        btnAuto.onClick.AddListener(() =>
        {
            bool flag = !playerStamina.IsAuto();
            playerStamina.ToggleAuto(flag);

            string str = flag ? "On" : "Off";
            txtAuto.text = "Auto: " + str;

            SceneLoading.Instance.ToggleTutFocus(false);
        });

        playerStamina.StartTraining();

        btnRun.onClick.AddListener(() =>
        {
            SceneLoading.Instance.ActiveLoadingScene();
            SceneManager.LoadScene(2);
            SceneLoading.Instance.ToggleTutFocus(false);
        });
    }


}
