using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStats playerStats;
    private PlayerController playerController;

    protected  void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerController = GetComponent<PlayerController>();
    }

    public PlayerStats PlayerStats { get { return playerStats; } }
    public PlayerController PlayerController { get { return playerController; } }
}
