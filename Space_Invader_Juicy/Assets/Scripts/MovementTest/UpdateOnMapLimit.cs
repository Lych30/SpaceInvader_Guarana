using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOnMapLimit : MonoBehaviour
{
    [SerializeField] GameObject otherWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Invader>())
        {
            EnemyMovement.Instance.UpdateTarget();
            otherWall.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
