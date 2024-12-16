using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private RectTransform clockwise;
    [SerializeField] private TextMeshProUGUI txtMax;

    private void Start()
    {
        txtMax.text = player.GetMaxSpeed().ToString();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (player == null) return;
        Vector3 eAngle = new Vector3(0, 0, player.GetCurrentSpeed() / Mathf.Max(player.GetMaxSpeed(), 0.01f) * 180);
        clockwise.localEulerAngles = Vector3.Lerp(clockwise.localEulerAngles, eAngle, 5f * Time.deltaTime); ;
    }

}
