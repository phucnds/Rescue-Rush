using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPosition : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private Slider sliderTsunami;
    [SerializeField] private RectTransform rect;
    [SerializeField] private Image icCat;

    private bool isActive = true;

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
        if (!isActive) return;
        UpdateUI();
    }

    private void UpdateUI()
    {
        float PosZ = player.GetPosZ();
        float lengthRoad = LevelManager.Instance.GetLengthP1();

        sliderPlayer.value = Mathf.Max(PosZ, 0) / lengthRoad;

        if (LevelManager.Instance.GetTsunami() == null) return;

        float tPosZ = LevelManager.Instance.GetTsunami().transform.position.z;

        sliderTsunami.value = Mathf.Max(tPosZ, 0) / lengthRoad;

        if (rect.transform.childCount == 0)
        {
            SetPosCat(LevelManager.Instance.LstCatThisLevel());
        }

        if (tPosZ >= PosZ)
        {
            GameManager.Instance.SetGameState(GameState.GAMEOVER);
            isActive = false;
        }
    }

    private void SetPosCat(List<Cat> lst)
    {
        float l = rect.rect.xMax - rect.rect.x;
        float lengthRoad = LevelManager.Instance.GetLengthP1();

        foreach (Cat cat in lst)
        {
            float posZ = cat.transform.position.z;
            float x = posZ / lengthRoad * l;

            Image ic = Instantiate(icCat, rect.transform);
            ic.transform.localPosition = new Vector2(x + rect.rect.x, 0);
        }

    }


}
