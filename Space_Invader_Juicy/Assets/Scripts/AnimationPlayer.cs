using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private AnimationClip idle;

    private Animator animator;

    private bool useAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            useAnimation = !useAnimation;
            animator.Play(useAnimation ? idle.name : "Rest");
        }
    }
}
