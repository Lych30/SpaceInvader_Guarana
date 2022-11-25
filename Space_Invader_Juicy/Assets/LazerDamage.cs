using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerDamage : MonoBehaviour
{
    [SerializeField]private ParticleSystem lazer;
    public List<ParticleCollisionEvent> collisionEvents;
    public float Dmg;
    private void Start()
    {

        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = lazer.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb && other.GetComponent<Invader>())
            {
                
                other.GetComponent<Invader>().TakeDmg(Dmg);//,force);

               
            }
            else if (rb && other.GetComponent<EnemyBullet>())
            {
             
                //Vector3 force = collisionEvents[i].velocity*10;
                other.GetComponent<EnemyBullet>().TakeDmg(Dmg);//,force);

            }
            i++;
        }
    }
}
