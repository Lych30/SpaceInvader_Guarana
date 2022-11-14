using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestTween : MonoBehaviour
{

    [SerializeField] GameObject objectToMove;

    void Start()
    {
        objectToMove.GetComponent<Rigidbody>().DOMove(new Vector3(2, 3, 4), 1).OnComplete(() =>
        {
            objectToMove.GetComponent<Material>().DOColor(Color.green, 1);
        });
    }


    void Update()
    {
        
    }
}
