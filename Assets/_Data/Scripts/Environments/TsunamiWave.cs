using UnityEngine;

public class TsunamiWave : MonoBehaviour
{
    private float moveSpeed = 0;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.TryGetComponent<Player>(out Player player))
        // {
        //     GameManager.Instance.SetGameState(GameState.GAMEOVER);

        //     Debug.Log("Player");
        // }
    }
}
