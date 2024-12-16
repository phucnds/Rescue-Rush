using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private RectTransform clockwise;
    [SerializeField] private TextMeshProUGUI txtMax;

    private void Start() {
        txtMax.text = player.GetMaxSpeed().ToString();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        Vector3 eAngle = new Vector3(0, 0, player.GetCurrentSpeed() / player.GetMaxSpeed() * 180);
        clockwise.localEulerAngles = Vector3.Lerp(clockwise.localEulerAngles, eAngle, 5f * Time.deltaTime); ;
    }

}
