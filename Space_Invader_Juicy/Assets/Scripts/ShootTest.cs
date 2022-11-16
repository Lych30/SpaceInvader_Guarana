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
    public GameObject EnnemyParts;
    ParticleSystemSubEmitterProperties subEmitter;
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
            if (rb && other.GetComponent<Invader>())
            {
                Debug.Log("Hit");
                
                Vector3 pos = collisionEvents[i].intersection;
                //Vector3 force = collisionEvents[i].velocity*10;
                other.GetComponent<Invader>().TakeDmg(Dmg);//,force);

                float random = Random.Range(0.0f, 1.0f);
                if (random >= 0.7)
                {
                    Debug.Log("SpawnParts");
                    GameObject parts = Instantiate(EnnemyParts, pos, Quaternion.identity);
                   Destroy(parts, 2);
                }
            }
            else if (rb && other.GetComponent<EnemyBullet>())
            {
                Debug.Log("Hit bullet");

                Vector3 pos = collisionEvents[i].intersection;
                //Vector3 force = collisionEvents[i].velocity*10;
                other.GetComponent<EnemyBullet>().TakeDmg(Dmg);//,force);

                float random = Random.Range(0.0f, 1.0f);
                if (random >= 0.7)
                {
                    Debug.Log("SpawnParts");
                    GameObject parts = Instantiate(EnnemyParts, pos, Quaternion.identity);
                    Destroy(parts, 2);
                }
            }
            i++;
        }
    }
}
