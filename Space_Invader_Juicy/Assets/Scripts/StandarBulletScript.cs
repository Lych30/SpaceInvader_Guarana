using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandarBulletScript : MonoBehaviour
{
    public float speed;
    public float Dmg;
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += Vector3.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.GetComponent<Invader>())
        {
            other.transform.gameObject.GetComponent<Invader>().TakeDmg(Dmg);
        }
        else if (other.transform.gameObject.GetComponent<EnemyBullet>())
        {
            other.transform.gameObject.GetComponent<EnemyBullet>().TakeDmg(Dmg);
        }
        Destroy(this.gameObject);
    }
}
