using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using Cinemachine;


public class Shoot : MonoBehaviour
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
    public AudioSource ShootSound_SpaceInvaders;
    public AudioSource GatlingSound;
    private float TimerBtwShots = 1.0f;
    private float Timer;
    public FuryBarScript furyBarScript;
    public GameObject LazerGO;
    public CinemachineImpulseSource LazerShake;


    //[SerializeField] private Animator anim;

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

        bool inputFire = Input.GetButton("Fire1");
        //anim.SetBool("Firing", inputFire && GameFeelManager.instance.FireGF);
        if (furyBarScript)
        {
            if(Input.GetKeyDown(KeyCode.L) && furyBarScript.FurySlider.value >= furyBarScript.FurySlider.maxValue)
            {
                //AHAH big lazer go BRRRRRRR
                Debug.Log("BRRRRRR");
                StartCoroutine(BigLazerGoBRRR());
            }
        }



        if (inputFire)
        {
            if(GameFeelManager.instance.ShakeGF)
                impulseSource.GenerateImpulse();

            if (GameFeelManager.instance.FireGF && !LazerGO.activeInHierarchy)
            {
                GatlingParticles(true);
                if(furyBarScript)
                    furyBarScript.AddFury(1);
                if (!GatlingSound.isPlaying)
                {
                    GatlingSound.Play();
                }
                else
                {
                    GatlingSound.pitch = Random.Range(1.0f,1.25f);
                }
            }
            else
            {
                GatlingParticles(false);
                if (Timer <= 0)
                {
                    //garbage shoot
                    ShootSound_SpaceInvaders.Play();
                    GameObject bullet = Instantiate(StandarBullet, transform.position, Quaternion.identity);
                    
                    Destroy(bullet, 3);
                    Timer = TimerBtwShots;
                }
            }
        }
        else
        {
            GatlingParticles(false);
            if (GatlingSound.isPlaying)
            {
                GatlingSound.Stop();
            }
        }
    }

    private void GatlingParticles(bool on)
    {
        part.enableEmission = on;
        MuzzleFlash.enableEmission = on;
        WhiteMuzzleFlash.enableEmission = on;
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
                Vector3 pos = collisionEvents[i].intersection;
                //Vector3 force = collisionEvents[i].velocity*10;
                other.GetComponent<Invader>().TakeDmg(Dmg);//,force);

                float random = Random.Range(0.0f, 1.0f);
                if (random >= 0.7)
                {
                    GameObject parts = Instantiate(EnnemyParts, pos, Quaternion.identity);
                    Destroy(parts, 2);
                }
            }
            else if (rb && other.GetComponent<EnemyBullet>())
            {
                Vector3 pos = collisionEvents[i].intersection;
                //Vector3 force = collisionEvents[i].velocity*10;
                other.GetComponent<EnemyBullet>().TakeDmg(Dmg);//,force);

                float random = Random.Range(0.0f, 1.0f);
                if (random >= 0.7)
                {
                    GameObject parts = Instantiate(EnnemyParts, pos, Quaternion.identity);
                    Destroy(parts, 2);
                }
            }
            i++;
        }
    }

    IEnumerator BigLazerGoBRRR()
    {
        LazerGO.SetActive(true);
        
        
        while (furyBarScript.FurySlider.value > 0)
        {
            LazerShake.GenerateImpulse();
            furyBarScript.AddFury(-10);
            yield return new WaitForFixedUpdate();
        }
        LazerGO.SetActive(false);
    }
}
