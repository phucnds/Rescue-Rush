using UnityEngine;

public class TutorialFocus : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject hand;

    private void Start()
    {
        LeanTween.scale(hand, Vector3.one * .9f, .4f).setEase(LeanTweenType.easeOutQuad).setLoopPingPong().setRepeat(-1);
        LeanTween.scale(circle, Vector3.one * .8f, .4f).setEase(LeanTweenType.easeOutQuad).setDelay(0.15f).setLoopPingPong().setRepeat(-1);
        
    }


}
