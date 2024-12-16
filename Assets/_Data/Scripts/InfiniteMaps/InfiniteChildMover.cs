using UnityEngine;

public class InfiniteChildMover : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float mapChunkSize;
    [SerializeField] private float distanceThreshold = 1.5f;

    private void Update()
    {
        UpdateChildren();
    }

    private void UpdateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            Vector3 distance = playerTransform.position - child.position;
            float calculatedDistanceThreshold = mapChunkSize * distanceThreshold;

            if (Mathf.Abs(distance.z) > calculatedDistanceThreshold)
            {
                child.position += Vector3.forward * calculatedDistanceThreshold * 2 * Mathf.Sign(distance.z);
            }
        }
    }
}
