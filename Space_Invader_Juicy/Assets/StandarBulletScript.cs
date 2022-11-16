using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandarBulletScript : MonoBehaviour
{
    Vector3 Direction = new Vector3(0, 1, 0);
    public float speed;
    public float Dmg;
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += Direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.GetComponent<Invader>())
        {
            Debug.Log("Hit");

           
            other.transform.gameObject.GetComponent<Invader>().TakeDmg(Dmg);//,force);

            
        }
        else if (other.transform.gameObject.GetComponent<EnemyBullet>())
        {
            Debug.Log("Hit bullet");

            
            other.transform.gameObject.GetComponent<EnemyBullet>().TakeDmg(Dmg);//,force);

            
        }
        Destroy(this.gameObject);
    }
}
