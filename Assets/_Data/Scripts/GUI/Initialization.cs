using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    private void Start()
    {
        SceneLoading.Instance.ActiveLoadingScene(() =>
        {
            SceneLoading.Instance.ToggleTutFocus(true);
        });

        SceneManager.LoadScene(1);

    }
}
