using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Transform visual;


    public void UpdateVisual()
    {
        int index = Random.Range(0, visual.childCount);

        for (int i = 0; i < visual.childCount; i++)
        {
            visual.GetChild(i).gameObject.SetActive(i == index);

        }
    }
}
