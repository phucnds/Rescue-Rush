using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private GameObject mapChunkPrefab;
    [SerializeField] private float mapChunkSize;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int z = -1; z <= 1; z++)
        {
            GenerateMapChunk(0, z);
        }
    }

    private void GenerateMapChunk(int x, int z)
    {
        Vector3 spawnPos = new Vector3(x, 0, z) * mapChunkSize;
        Instantiate(mapChunkPrefab, spawnPos, Quaternion.identity, transform);
    }
}
