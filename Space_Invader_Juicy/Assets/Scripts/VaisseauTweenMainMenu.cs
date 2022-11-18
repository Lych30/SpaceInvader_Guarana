using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VaisseauTweenMainMenu : MonoBehaviour
{
    Vector3 startPosition;

    //Vector2 randomPos;
    float randomPosX;
    float randomPosY;
    
    float offSetX;
    float offSetY;
    
    float durationToNewPos;
    float durationToStartPos;

    Vector3 actualPosition;

    Animator animator;

    float speed;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        RandomMove();
    }

    public void RandomMove()
    {
        randomPosX = Random.Range(-2,2);
        randomPosY = Random.Range(-2, 2);
        durationToNewPos = Random.Range(5f, 10f);
        transform.GetComponent<Rigidbody>().DOMove(new Vector3(randomPosX, randomPosY, 15), durationToNewPos).OnComplete(() =>
        {
            BackToStartPosition();
        });
    }

    public void BackToStartPosition()
    {
        offSetX = Random.Range(-2f,2f);
        offSetY = Random.Range(-2f, 2f);
        durationToStartPos = Random.Range(5f, 10f);

        /*if ()
        {

        }*/

        transform.GetComponent<Rigidbody>().DOMove(new Vector3(startPosition.x, startPosition.y, startPosition.z) + new Vector3(offSetX, offSetY, 0), durationToStartPos).OnComplete(() =>
        {
            RandomMove();
        });
    }

}
