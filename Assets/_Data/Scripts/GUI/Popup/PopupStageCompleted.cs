using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupStageCompleted : Popup
{
    [SerializeField] Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            CurrencyManager.Instance.AddCurrency(500);
            Close();

            SceneLoading.Instance.ActiveLoadingScene();
            SceneManager.LoadScene(1);
        });
    }
}
