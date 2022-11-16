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
    public GameObject StandarBullet;
   
    private float TimerBtwShots = 1.0f;
    private float Timer;
    void Start()
    {
        Timer = TimerBtwShots;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        part = GetComponent<ParticleSystem>();
        part.enableEmission = false;
        MuzzleFlash.enableEmission = false;
        WhiteMuzzleFlash.enableEmission = false;
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void Update()
    {
        if(Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }
        
        if (Input.GetButton("Fire1"))
        {
            if(GameFeelManager.instance.ShakeGF)
                impulseSource.GenerateImpulse();


            if (GameFeelManager.instance.FireGF)
            {
                part.enableEmission = true;
                MuzzleFlash.enableEmission = true;
                WhiteMuzzleFlash.enableEmission = true;
            }
            else
            {
                if(Timer <= 0)
                {
                    //garbage shoot
                    GameObject bullet = Instantiate(StandarBullet, transform.position, Quaternion.identity);
                    Destroy(bullet, 3);
                    Timer = TimerBtwShots;
                }

            }

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
