using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyScript : MonoBehaviour
{
    public float Life;
    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void TakeDmg(float dmg,Vector3 Force)
    {
        Life -= dmg;
        if(Life <= 0)
        {
            //faudra mettre de la juicyness ici
            Destroy(this.gameObject,2);
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(Force);
        }
    }
}
