using TMPro;
using UnityEngine;

public class CurrencyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCurrency;

    public void UpdateText(string currencyText)
    {
        txtCurrency.text = currencyText;
    }
}