using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void UpdateAnimator(Vector3 moveVector,float velocity)
    {
        // Debug.Log(velocity);
        anim.SetFloat("moveSpeed", velocity);
        anim.transform.forward = moveVector.normalized;
    }
}
