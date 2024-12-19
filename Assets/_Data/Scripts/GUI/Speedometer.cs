using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour, IPlayerStatsDepnedency, IGameStateListener
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private RectTransform clockwise;
    [SerializeField] private TextMeshProUGUI txtMax;
    [SerializeField] private TextMeshProUGUI txtStamia;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject contain;

    private PlayerStamina playerTraining;
    private float speed = 5f;

    float timer = 0;

    private void Awake()
    {
        playerTraining = player.GetComponent<PlayerStamina>();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (player == null) return;

        // Debug.Log($"{player.GetCurrentSpeed()} / {player.GetMaxSpeed()} * 180 = {player.GetCurrentSpeed() / player.GetMaxSpeed() * 180}");

        Vector3 eulerAngles = new Vector3(0, 0, player.GetCurrentSpeed() / Mathf.Max(player.GetMaxSpeed(), 0.01f) * 180);
        clockwise.localEulerAngles = Vector3.Lerp(clockwise.localEulerAngles, eulerAngles, speed * Time.deltaTime);

        txtStamia.text = playerTraining.DisplayStamina();
        slider.value = playerTraining.StanimaThreshold();
    }

    public void UpdateStats(PlayerStats playerStats)
    {
        txtMax.text = playerStats.GetValueStat(Stat.SPEED).ToString();
        UpdateUI();
    }

    public void GameStateChangeCallback(GameState gameState)
    {
        contain.SetActive(false);
        switch (gameState)
        {

            case GameState.GAME:
            case GameState.TRAINING:
            case GameState.PHASECOMPLETE:
                contain.SetActive(true);
                break;
        }
    }
}
