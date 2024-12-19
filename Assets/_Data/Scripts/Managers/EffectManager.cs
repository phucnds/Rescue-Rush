using System;
using UnityEngine;
using UnityEngine.Pool;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Transform income;
    [SerializeField] private Transform speed;
    [SerializeField] private Transform stamina;

    [SerializeField] private TextFading textFadingPrefabs;

    private ObjectPool<TextFading> textFadingPool;

    private void Awake()
    {
        PlayerStamina.OnShowTextFading += PlayerStamina_OnShowTextFading;
    }

    private void OnDestroy()
    {
        PlayerStamina.OnShowTextFading -= PlayerStamina_OnShowTextFading;
    }


    private void Start()
    {
        textFadingPool = new ObjectPool<TextFading>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private TextFading CreateFunction() => Instantiate(textFadingPrefabs, income);

    private void ActionOnGet(TextFading text) => text.gameObject.SetActive(true);

    private void ActionOnRelease(TextFading text)
    {
        if (text == null) return;
        text.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(TextFading text) => Destroy(text.gameObject);


    private void PlayerStamina_OnShowTextFading(Stat stat, float value)
    {
        TextFading tf = textFadingPool.Get();
        Sprite ic = ResourceManager.Instance.PlayerStatsData.GetListStat()[stat].Icon;
        tf.transform.SetParent(GetParent(stat));
        tf.ResetPosition();
        tf.Setup(ic, value);
        LeanTween.delayedCall(.5f, () => textFadingPool.Release(tf));
    }

    private Transform GetParent(Stat stat)
    {
        switch (stat)
        {

            case Stat.STAMINA: return stamina;
            case Stat.SPEED: return speed;
            case Stat.INCOME: return income;
        }

        return income;
    }
}
