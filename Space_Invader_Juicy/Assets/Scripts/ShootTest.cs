using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;


public class ShootTest : MonoBehaviour
{
    public float Dmg;
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void OnParticleCollision(GameObject other)
    {
        
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb && other.GetComponent<EnnemyScript>())
            {
                Debug.Log("Hit");
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity*10;
                other.GetComponent<EnnemyScript>().TakeDmg(Dmg,force);
                

            }
            i++;
        }
    }
}
