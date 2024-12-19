using UnityEngine;

public class StatsText : MonoBehaviour
{
    [SerializeField] private TextFading textFading;


    [NaughtyAttributes.Button]
    private void Spawn()
    {
        Sprite ic = ResourceManager.Instance.PlayerStatsData.GetListStat()[Stat.STAMINA].Icon;

        TextFading tf = Instantiate(textFading, transform);
        tf.Setup(ic, 100);
    }
}
