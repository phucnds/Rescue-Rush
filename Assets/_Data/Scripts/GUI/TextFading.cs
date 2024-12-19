using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextFading : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtValue;
    [SerializeField] private Image icon;
    [SerializeField] private RectTransform rect;

    private Vector3 initPos;

    private void Start()
    {
        initPos = rect.localPosition; ;
    }

    public void Setup(Sprite ic, float value)
    {
        icon.sprite = ic;
        icon.gameObject.SetActive(true);
        txtValue.text = value.ToString();
        txtValue.gameObject.SetActive(true);

        LeanTween.moveY(rect, 150, .5f).setEase(LeanTweenType.easeOutCirc).setOnComplete(() =>
        {
            txtValue.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);


        });
    }

    public void ResetPosition()
    {
        rect.localPosition = initPos;
    }
}
