using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public GameObject bowCrosshair;
    public GameObject gunCrosshair;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void WalkingAnimation(bool _flag)
    {
        animator.SetBool("Walking", _flag);
    }

    public void FireAnimation()
    {
        if (!animator.GetBool("Walking"))
        {
            animator.SetTrigger("Fire");
        }
    }
}