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
     
        if(useAnimation != GameFeelManager.instance.EnnemyAnimGF)
        {
            useAnimation = GameFeelManager.instance.EnnemyAnimGF;
            animator.Play(useAnimation ? idle.name : "Rest");
        }
            

    }
}
