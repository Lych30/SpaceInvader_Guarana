using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private AnimationClip idle;

    private Animator[] animatorList;
    //[SerializeField] private Animator animator;
    //[SerializeField] private Animator animatorVR;

    private bool useAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        animatorList = GetComponentsInChildren<Animator>();
    }

    private void Update()
    {
     
        if(useAnimation != GameFeelManager.instance.EnnemyAnimGF)
        {
            useAnimation = GameFeelManager.instance.EnnemyAnimGF;

            foreach (var item in animatorList)
            {
                item.Play(useAnimation ? idle.name : "Rest");
            }

            //animator.Play(useAnimation ? idle.name : "Rest");
            //animatorVR.Play(useAnimation ? idle.name : "Rest");
        }
            

    }
}
