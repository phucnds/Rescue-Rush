using System.Collections;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private float fadeSpeed;

    public virtual void Show()
    {
        bg.interactable = true;
        bg.blocksRaycasts = true;
        bg.transform.localScale = Vector3.zero;
        bg.alpha = 1;
        LeanTween.scale(bg.gameObject, Vector3.one, .6f).setEase(LeanTweenType.easeOutBack);

    }

    public virtual void Close()
    {
        StartCoroutine(FadeRoutine(0));
        bg.interactable = false;
        bg.blocksRaycasts = false;
    }


    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(bg.alpha, targetAlpha))
        {
            bg.alpha = Mathf.MoveTowards(bg.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
