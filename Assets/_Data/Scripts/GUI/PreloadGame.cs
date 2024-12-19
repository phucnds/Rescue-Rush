using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreloadGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtNum;
    [SerializeField] private Button tapToSpeedUp;

    int count = 3;

    private void Start()
    {
        count = 3;
        txtNum.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }


    private IEnumerator CountDown()
    {

        while (count > 0)
        {
            ShowText();
            yield return new WaitForSeconds(1);
            count--;
        }

        GameManager.Instance.StartGame();
    }


    private void ShowText()
    {
        txtNum.text = count.ToString();
        txtNum.rectTransform.localScale = Vector3.zero;
        LeanTween.scale(txtNum.rectTransform, Vector3.one, .6f).setEase(LeanTweenType.easeOutBack);
    }
}
