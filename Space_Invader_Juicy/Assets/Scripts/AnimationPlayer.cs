using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private AnimationClip idle;

    private Animation anim;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play(idle.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
