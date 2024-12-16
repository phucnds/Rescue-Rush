using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    public bool canSee;

    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] private GameObject fovRenderer;

    [SerializeField] private Transform graphic;
    [SerializeField] private PlayerCarry playerCarry;

    private Cat catFounded = null;

    private void Update()
    {
        FieldOfViewCheck();
    }

    private List<Cat> GetListCatInRange()
    {
        List<Cat> lst = new List<Cat>();

        Collider[] rangeChecks = Physics.OverlapSphere(graphic.position, radius, targetMask);
        foreach (Collider col in rangeChecks)
        {
            if (col.TryGetComponent<Cat>(out Cat cat) && !playerCarry.GetlistCat().Contains(cat))
            {
                lst.Add(cat);
            }
        }

        return lst;
    }

    private void FieldOfViewCheck()
    {
        List<Cat> rangeChecks = GetListCatInRange();

        if (rangeChecks.Count != 0)
        {
            Cat first = rangeChecks[0];
            Vector3 directionToTarget = (first.transform.position - graphic.position).normalized;

            if (Vector3.Angle(graphic.forward, directionToTarget) < angle / 2) { FoundCat(first, directionToTarget); }
            else if (canSee) { IgnoreCat(); }
        }
        else if (canSee) { IgnoreCat(); }

        fovRenderer.SetActive(canSee);
    }

    private void IgnoreCat()
    {
        canSee = false;
        if (catFounded != null) catFounded.ResetProgression();
        catFounded = null;
    }

    private void FoundCat(Cat target, Vector3 directionToTarget)
    {
        Debug.Log("FoundCat");

        float distanceToTarget = Vector3.Distance(graphic.position, target.transform.position);
        canSee = !Physics.Raycast(graphic.position, directionToTarget, distanceToTarget, obstructionMask);

        if (canSee)
        {
            if (catFounded == null)
            {
                catFounded = target;
            }

            catFounded.SetProgression(Time.deltaTime);

            if (catFounded.IsCaptured())
            {
                playerCarry.AddCat(catFounded);
                catFounded = null;
            }
        }
    }
}
