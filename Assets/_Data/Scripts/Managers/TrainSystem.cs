using UnityEngine;
using UnityEngine.UI;

public class TrainSystem : MonoBehaviour
{
    [SerializeField] private Button touchZone;

    private void Start()
    {
        touchZone.onClick.AddListener(() =>
        {
            Debug.Log("das");
        });
    }
}
