using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using Cinemachine;


public class ShootTest : MonoBehaviour
{
    public float Dmg;
    public ParticleSystem part;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem WhiteMuzzleFlash;
    CinemachineImpulseSource impulseSource;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
     
        impulseSource = GetComponent<CinemachineImpulseSource>();
        part = GetComponent<ParticleSystem>();
        part.enableEmission = false;
        MuzzleFlash.enableEmission = false;
        WhiteMuzzleFlash.enableEmission = false;
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            impulseSource.GenerateImpulse();
            part.enableEmission = true;
            MuzzleFlash.enableEmission = true;
            WhiteMuzzleFlash.enableEmission = true;
        }
        else
        {
            part.enableEmission = false;
            MuzzleFlash.enableEmission = false;
            WhiteMuzzleFlash.enableEmission = false;
        }
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
