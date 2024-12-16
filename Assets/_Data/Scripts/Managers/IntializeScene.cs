using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntializeScene : MonoBehaviour
{
    [SerializeField] private Button btnNewGame;
    [SerializeField] private Button btnContinue;

    private void Start()
    {
        btnNewGame.onClick.AddListener(() => {
            LoadSceneGame();
        });

        btnContinue.onClick.AddListener(() =>
        {
            LoadSceneGame();
        });
    }

    private void LoadSceneGame()
    {
        SceneManager.LoadScene(1);
    }
}
