using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPosition : MonoBehaviour
{
    [SerializeField] PlayerController player;

    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private Slider sliderTsunami;
    [SerializeField] private RectTransform rect;
    [SerializeField] private Image icCat;

    private List<Cat> cats;

    private void Start()
    {
        LevelManager.Instance.OnSetPositionCat += SetPosCat;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnSetPositionCat -= SetPosCat;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        float PosZ = player.GetPosZ();
        float lengthRoad = LevelManager.Instance.endPointPhase1.z;

        sliderPlayer.value = Mathf.Max(PosZ, 0) / lengthRoad;

        if (LevelManager.Instance.GetTsunami() == null) return;

        float tPosZ = LevelManager.Instance.GetTsunami().transform.position.z;

        sliderTsunami.value = Mathf.Max(tPosZ, 0) / lengthRoad;


    }

    private void SetPosCat(List<Cat> lst)
    {
        cats = lst;

        float l = rect.rect.xMax - rect.rect.x;
        float lengthRoad = LevelManager.Instance.endPointPhase1.z;

        foreach (Cat cat in cats)
        {
            float posZ = cat.transform.position.z;
            float x = posZ / lengthRoad * l;

            Image ic = Instantiate(icCat, rect.transform);
            ic.transform.localPosition = new Vector2(x + rect.rect.x, 0);
        }

    }


}
