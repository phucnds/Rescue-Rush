using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [SerializeField] private List<Cat> lstCat = new List<Cat>();
    [SerializeField] private Transform catCarryParent;
    [SerializeField] private float offset = 6;

    public List<Cat> GetlistCat()
    {
        return lstCat;
    }

    public void AddCat(Cat cat)
    {
        lstCat.Add(cat);
        cat.transform.SetParent(null);
        cat.transform.SetParent(catCarryParent);
        cat.transform.localPosition = new Vector3(0, (lstCat.Count - 1) * 2 + offset, 0);
        cat.transform.forward = transform.forward;

        LevelManager.Instance.OnRescueComplete(cat);
        // cat.gameObject.SetActive(false);
    }
}
