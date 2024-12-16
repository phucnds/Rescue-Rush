using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [field: SerializeField] public PlayerStatsData PlayerStatsData { get; private set; }
}
