using UnityEngine;

public class TsunamiWave : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
