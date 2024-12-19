using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoading : Singleton<SceneLoading>
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI txtLoading;
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private float duration = 2f;

    [SerializeField] private GameObject TutFocus;

    private int maxValue = 100;
    private float timer = 0;
    private bool isActive = false;

    private Action onCompleted;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        UpdateUI();

        if (!isActive) return;

        timer += Time.deltaTime * maxValue / duration;
        timer = Mathf.Clamp(timer, 0, maxValue);
        if (timer >= maxValue)
        {
            isActive = false;
            onCompleted?.Invoke();
            StartCoroutine(FadeRoutine(0));
        }
    }

    private void UpdateUI()
    {
        slider.value = timer / maxValue;
        txtLoading.text = $"Loading... {Mathf.RoundToInt(timer)}%";

    }

    [NaughtyAttributes.Button]
    public void ActiveLoadingScene(Action onCompleted = null)
    {
        timer = 0;
        isActive = true;
        this.onCompleted = onCompleted;
        bg.gameObject.SetActive(true);
        bg.alpha = 1;

    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(bg.alpha, targetAlpha))
        {
            bg.alpha = Mathf.MoveTowards(bg.alpha, targetAlpha, Time.deltaTime / .2f);
            yield return null;
        }
    }

    public void ToggleTutFocus(bool flag)
    {
        TutFocus.SetActive(flag);
    }
}
