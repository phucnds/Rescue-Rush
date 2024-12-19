using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public Vector3 StartPos { get; private set; } = new Vector3(50, 0, 0);
    [field: SerializeField] public Vector3 EndPointPhase1 { get; private set; } = new Vector3(50, 0, 400);
    [field: SerializeField] public Vector3 EndPointPhase2 { get; private set; } = new Vector3(50, 0, 1400);

    [SerializeField] private Segment[] segmentPrefabs;
    [SerializeField] private GameObject roadP2Prefab;
    [SerializeField] private GameObject roadCornerPrefab;
    [SerializeField] private int amountObstacle;

    [SerializeField] private TsunamiWave tsunamiWavePrefab;
    [SerializeField] private Vector3 tsunaminPos = new Vector3(50, 0, -150);
    [SerializeField] private float moveSpeed = 10;

    private int amountSegment = 5;

    private int lSegment = 80;
    private int lRoad2 = 200;

    private int lengthP1;
    private int lengthP2;

    private int type;

    private TsunamiWave tsunamiWave;

    private List<Cat> lstCat = new List<Cat>();

    public void SetupLevel(int amountObs, float mSpeed)
    {
        amountObstacle = amountObs;
        moveSpeed = mSpeed;
    }

    [NaughtyAttributes.Button]
    public void BuildLevel()
    {
        Init();
        
        Clear();

        BuildCorner();

        BuildPhase1();

        BuildPhase2();

        CreateTsunamiWave();
    }

    private void Init()
    {
        lengthP1 = Mathf.RoundToInt(Vector3.Distance(StartPos, EndPointPhase1));
        lengthP2 = Mathf.RoundToInt(Vector3.Distance(StartPos, EndPointPhase2));

        type = Random.Range(0, segmentPrefabs.Length);
    }

    private void BuildCorner()
    {
        Instantiate(roadCornerPrefab,transform);
    }

    private void BuildPhase1()
    {
        int[] result = GenerateArray(amountObstacle);

        for (int i = 0; i < amountSegment; i++)
        {
            bool hasObstacle = result[i] == 1;
            Segment segment = Instantiate(segmentPrefabs[type], new Vector3(0, 0, i * lSegment), Quaternion.identity, transform);
            segment.CreateSegment(hasObstacle);

            lstCat.Add(segment.GetCat());
        }
    }

    private void BuildPhase2()
    {
        int count = (lengthP2 - lengthP1) / lRoad2;

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(0, 0, i * lRoad2 + EndPointPhase1.z);
            GameObject road = Instantiate(roadP2Prefab, pos, Quaternion.identity, transform);
        }
    }

    private void CreateTsunamiWave()
    {
        tsunamiWave = Instantiate(tsunamiWavePrefab, tsunaminPos, Quaternion.identity, transform);

    }

    public void StartTsunamiWave() => tsunamiWave.SetMoveSpeed(moveSpeed);
    public void StopTsunamiWave() => tsunamiWave.SetMoveSpeed(0);
    public TsunamiWave TsunamiWave { get { return tsunamiWave; } }

    private void Clear()
    {
        foreach (Transform tr in transform)
        {
            Destroy(tr.gameObject);
        }
    }

    private int[] GenerateArray(int amount)
    {
        int[] array = new int[5];

        List<int> arrNew = new List<int> { 1, 2, 3, 4 };

        for (int i = 0; i < arrNew.Count; i++)
        {
            int radIndex = Random.Range(i, arrNew.Count);
            int temp = arrNew[i];
            arrNew[i] = arrNew[radIndex];
            arrNew[radIndex] = temp;
        }

        for (int i = 0; i < amount; i++)
        {
            array[arrNew[i]] = 1;
        }

        return array;
    }

    public List<Cat> GetCats()
    {
        return lstCat;
    }

    public float GetLengthP1()
    {
        return lengthP1;
    }
}
