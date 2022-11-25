using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuVaisseau : MonoBehaviour
{

    Rigidbody rb;

    bool goRight = true;

    public float speed;
    public float rotateValue;
    public float clampVelocityValue;
    public float rotateDuration;

    public void Start()
    {
        goRight = true;
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        UpdateVelocity();

        ChangeVelocity();
    }

    public void UpdateVelocity()
    {
        if (goRight)
        {
            rb.velocity += new Vector3(Time.deltaTime * speed, 0, 0);
        }
        else
        {
            rb.velocity -= new Vector3(Time.deltaTime * speed, 0, 0);
        }
    }

    public void ChangeVelocity()
    {
        if ((rb.velocity.x > clampVelocityValue) && goRight)
        {
            rb.velocity = new Vector3(clampVelocityValue, rb.velocity.y, rb.velocity.z);
            goRight = false;
            speed = Random.Range(1f, 2f);
            ChangeAngle(true);
        }
        else if ((rb.velocity.x < -clampVelocityValue) && !goRight)
        {
            rb.velocity = new Vector3(-clampVelocityValue, rb.velocity.y, rb.velocity.z);
            goRight = true;
            speed = Random.Range(1f, 2f);
            clampVelocityValue = Random.Range(1f, 3f);
            ChangeAngle(false);
        }
    }

    public void ChangeAngle(bool rotateRight)
    {
        //Vector3 rotateVector = new Vector3(rotateValue, transform.rotation.y, transform.localEulerAngles.z);
        Vector3 rotateVector = new Vector3(rotateValue, transform.rotation.y, -90);

        if (rotateRight)
        {
            transform.DORotate(rotateVector, rotateDuration);
        }
        else
        {
            transform.DORotate(new Vector3(-rotateVector.x, rotateVector.y, rotateVector.z), rotateDuration);
        }
    }
}
