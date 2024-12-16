using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [Header("Road")]
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Transform roadParent;
    [SerializeField] private int lengthPhase1 = 400;
    [SerializeField] private int lengthPhase2 = 400;
    [NaughtyAttributes.HorizontalLine]

    [Header("Cats")]
    [SerializeField] private Vector2[] lstPosCat;
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform catParent;
    [NaughtyAttributes.HorizontalLine]

    [Header("Tsunami")]
    [SerializeField] private GameObject tsunamiPrefab;
    [SerializeField] private Transform tsunamiParent;
    [SerializeField] private Vector3 tsunamiPos = new Vector3(50, 0, -150);
    [NaughtyAttributes.HorizontalLine]

    [Header("Player")]
    [SerializeField] private PlayerController player;
    [Space(20)]

    private int lengthRoad = 200;

    [NaughtyAttributes.Button]
    private void GenerateRoad()
    {
        foreach (Transform tr in roadParent)
        {
            Destroy(tr.gameObject);
        }

        int countP1 = lengthPhase1 / lengthRoad;

        for (int i = 0; i < countP1; i++)
        {
            Vector3 pos = new Vector3(0, 0, i * lengthRoad);
            GameObject road = Instantiate(roadPrefab, pos, Quaternion.identity, roadParent);
        }

        int countP2 = lengthPhase2 / lengthRoad;

        for (int i = countP1; i < countP2 + countP1; i++)
        {
            Vector3 pos = new Vector3(0, 0, i * lengthRoad);
            GameObject road = Instantiate(roadPrefab, pos, Quaternion.identity, roadParent);
        }

    }

    [NaughtyAttributes.Button]
    private void GenerateCat()
    {
        foreach (Transform tr in catParent)
        {
            Destroy(tr.gameObject);
        }

        for (int i = 0; i < lstPosCat.Length; i++)
        {
            GameObject cat = Instantiate(catPrefab, new Vector3(lstPosCat[i].x, 0, lstPosCat[i].y), Quaternion.identity, catParent);
        }
    }

    [NaughtyAttributes.Button]
    private void CreateTsunami()
    {
        foreach (Transform tr in tsunamiParent)
        {
            Destroy(tr.gameObject);
        }

        GameObject go = Instantiate(tsunamiPrefab, tsunamiPos, Quaternion.identity, tsunamiParent);
    }

    [NaughtyAttributes.Button]
    private void FindCat()
    {
        Vector3[] listPoint = new Vector3[lstPosCat.Length];
        for (int i = 0; i < listPoint.Length; i++)
        {
            listPoint[i] = new Vector3(lstPosCat[i].x, 0, lstPosCat[i].y);
        }

        player.StarMovement(listPoint);
    }

    [NaughtyAttributes.Button]
    private void MoveToGoal() => player.MoveToGoal();


}
