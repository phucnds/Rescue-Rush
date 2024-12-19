using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupGameOver : Popup
{
    [SerializeField] Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            CurrencyManager.Instance.AddCurrency(200);
            Close();

            SceneLoading.Instance.ActiveLoadingScene();
            SceneManager.LoadScene(1);
        });
    }
}
