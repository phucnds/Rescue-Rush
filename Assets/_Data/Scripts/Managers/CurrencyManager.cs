using System;
using Saving;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>,IWantToBeSaved
{
    [field: SerializeField] public float Currency { get; private set; }
    [SerializeField] private float initCurrency = 1000;


    const string CurrencyKey = "Currency";

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

    [NaughtyAttributes.Button]
    private void Add50000()
    {
        AddCurrency(50000);
    }

    public void Load()
    {
        if(SaveSystem.TryLoad(this, CurrencyKey, out object currencyValue))
        {
            AddCurrency((float)currencyValue);
        }
        else
        {
            AddCurrency(initCurrency);
        }
    }

    public void Save()
    {
        SaveSystem.Save(this,CurrencyKey, Currency);
    }
}
