using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private AnimationCurve lerpSpeed;
    [SerializeField] private Transform target;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float t;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        ChangeTarget();
    }

    // Update is called once per frame
    void Update()
    {
        LerpMovement();

        if (Vector2.Distance(transform.position, targetPosition) <= .025f)
            ChangeTarget();
    }

    void ChangeTarget()
    {
        startPosition = transform.position;
        targetPosition = target.position;
        t = 0f;
        distance = Vector2.Distance(startPosition, targetPosition);
    }

    void LerpMovement()
    {
        t = Time.deltaTime * speed / (distance + 1) + t;
        transform.position = Vector3.LerpUnclamped(startPosition, targetPosition, lerpSpeed.Evaluate(t));
    }
}
