using System;
using Saving;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [field: SerializeField] public float Currency { get; private set; }

    const string CurrencyKey = "Currency";

    private void Start()
    {
        AddCurrency(100);
    }

    public static Action onUpdated;

    public void AddCurrency(float amount)
    {
        Currency += amount;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        UpdateTexts();
        onUpdated?.Invoke();

        Save();
    }


    private void UpdateTexts()
    {
        CurrencyText[] currencyTexts = FindObjectsByType<CurrencyText>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (CurrencyText text in currencyTexts)
        {
            text.UpdateText(Currency.ToString());
        }
    }

    public bool HasEnoughCurrency(float price) => Currency >= price;
    public void UseCurrency(float price) => AddCurrency(-price);

    public void Load()
    {

    }

    public void Save()
    {

    }
}
