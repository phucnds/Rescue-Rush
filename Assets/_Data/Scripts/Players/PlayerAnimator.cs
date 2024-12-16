using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeedMultipler = .25f;

    // [SerializeField] private ParticleSystem waterParticle;

    public void ManageAnimations(Vector3 moveVector)
    {
        if (moveVector.magnitude > 0)
        {
            anim.SetFloat("moveSpeed", moveVector.magnitude * moveSpeedMultipler);

            PlayRunAnimation();

            anim.transform.forward = moveVector.normalized;
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayRunAnimation()
    {

        anim.Play("Char_Running");
    }

    private void PlayIdleAnimation()
    {
        anim.Play("Char_idle");
    }
}
