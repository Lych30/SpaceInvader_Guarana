using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
     public static EnemyMovement Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private AnimationCurve lerpSpeed;

    [SerializeField] public InvaderPath path;

    int targetIndex = 0;
    float t = 0;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float distance;
    private bool lerpToNextTarget = true;
    private bool moveDown;

    public void Start()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        if (path == null) return;
        UpdateTarget();
    }

    void Update()
    {
        if (path == null) return;

        if (lerpToNextTarget)
            LerpMovement();
        else
            Teleport();

        if (Vector2.Distance(transform.position, targetPosition) <= .025f)
        {
            UpdateTarget();
        }
    }

    public void Teleport()
    {
        transform.position = targetPosition;
    }

    public void UpdateTarget()
    {
        t = 0f;
        startPosition = transform.position;
        (targetPosition, targetIndex) = path.GetNextPosition(targetIndex);//, out lerpToNextTarget);

        if (moveDown)
        {
            targetPosition = new Vector3(startPosition.x, targetPosition.y, startPosition.z);
        }

        distance = Vector2.Distance(startPosition, targetPosition);
    }

    public void LerpMovement()
    {
        if (targetIndex <= -1) return;

        t = Time.deltaTime * speed / (distance + 1) + t;
        transform.position = Vector3.LerpUnclamped(startPosition, targetPosition, lerpSpeed.Evaluate(t));
    }
}
