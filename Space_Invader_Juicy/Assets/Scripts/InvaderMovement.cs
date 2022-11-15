using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private AnimationCurve lerpSpeed;

    [SerializeField] public Transform target;

    float t = 0;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float distance;

    public void Start()
    {
        if (target == null) return;
        UpdateTarget();
    }

    void Update()
    {
        if (target == null) return;
        LerpMovement();

        if (Vector2.Distance(transform.position, targetPosition) <= .025f)
        {
            StepDown();
            UpdateTarget();
        }
            
    }

    public void StepDown()
    {
        transform.position = new Vector3(   transform.position.x,
                                            transform.position.y - 2, 
                                            transform.position.z);
    }

    public void UpdateTarget()
    {
        t = 0f;
        startPosition = transform.position;
        targetPosition = target.position;
        distance = Vector2.Distance(startPosition, targetPosition);
    }

    public void LerpMovement()
    {
        t = Time.deltaTime * speed / (distance + 1) + t;
        transform.position = Vector3.LerpUnclamped(startPosition, targetPosition, lerpSpeed.Evaluate(t));
    }
}
