using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private AnimationCurve lerpSpeed;
    [SerializeField] private Transform target;

    [Header("Movement")]
    [SerializeField] private float healthPoints = 20f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float t;
    private float distance;

    private float life;

    // Start is called before the first frame update
    void Start()
    {
        life = healthPoints;

        if (target == null) return;

        //targetPosition = target.position;
        ChangeTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        LerpMovement();

        if (Vector2.Distance(transform.position, targetPosition) <= .025f)
            ChangeTarget();
    }

    void ChangeTarget()
    {
        startPosition = transform.position;
        targetPosition = target.position;
        //targetPosition = new Vector3(-targetPosition.x, targetPosition.y, targetPosition.z);
        t = 0f;
        distance = Vector2.Distance(startPosition, targetPosition);
    }

    void LerpMovement()
    {
        t = Time.deltaTime * speed / (distance + 1) + t;
        transform.position = Vector3.LerpUnclamped(startPosition, targetPosition, lerpSpeed.Evaluate(t));
    }

    public void TakeDmg(float dmg)//, Vector3 Force)
    {
        life -= dmg;
        if (life <= 0)
        {
            //faudra mettre de la juicyness ici
            Destroy(this.gameObject, 2);
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<Rigidbody>().AddForce(Force);
        }
    }
}
