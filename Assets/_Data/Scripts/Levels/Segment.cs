using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private Cat catPrefab;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform catParent;

    [SerializeField] private Obstacle obstaclePrefabs;

    private Cat cat;

    public void CreateSegment(bool hasObstacle)
    {
        CreatCat();
        if (!hasObstacle) return;
        CreateObstacle();
    }

    private void CreatCat()
    {
        Vector3 pos = RandomPos() + centerPoint.position;
        cat = Instantiate(catPrefab, pos, Quaternion.identity, catParent);
    }

    public void CreateObstacle()
    {
        Vector3 pos = cat.transform.position;

        Vector2 direction = Random.onUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(15, 20);
        Vector3 targetPos = pos + new Vector3(offset.x, 0, offset.y);
        Obstacle obstacle = Instantiate(obstaclePrefabs, targetPos, Quaternion.Euler(0, Random.Range(-180f, 180f), 0), transform);
        obstacle.UpdateVisual();
    }

    private Vector3 RandomPos()
    {
        float w = centerPoint.localScale.x;
        float h = centerPoint.localScale.z;

        float randX = Random.Range(-w / 2, w / 2);
        float randY = Random.Range(-h / 2, h / 2);
        // Debug.Log($"{w} - {h}");
        return new Vector3(randX, 0, randY);
    }

    public Cat GetCat()
    {
        return cat;
    }
}
