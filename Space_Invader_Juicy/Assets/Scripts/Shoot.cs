using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public InputActionAsset test;
    
    public GameObject[] GatlingArray;
    [SerializeField]ParticleSystem[] part;
    [SerializeField] ParticleSystem[] MuzzleFlash;
    [SerializeField] ParticleSystem[] WhiteMuzzleFlash;
    CinemachineImpulseSource impulseSource;
    public GameObject StandarBullet;
    public AudioSource ShootSound_SpaceInvaders;
    public AudioSource GatlingSound;
    private float TimerBtwShots = 1.0f;
    private float Timer;
    public FuryBarScript furyBarScript;
    public GameObject LazerGO;
    CinemachineImpulseSource LazerShake;

    public InputActionReference rightHandTrigger;
    public InputActionReference leftHandTrigger;
    private InputAction rightTriggerAction;
    private InputAction leftTriggerAction;

    //[SerializeField] private Animator anim;

    void Start()
    {
        Timer = TimerBtwShots;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        LazerShake = LazerGO.GetComponent<CinemachineImpulseSource>();

        //Arrays Init
        part = new ParticleSystem[GatlingArray.Length];
        MuzzleFlash = new ParticleSystem[GatlingArray.Length];
        WhiteMuzzleFlash = new ParticleSystem[GatlingArray.Length];
        for (int i = 0; i < GatlingArray.Length;i++)
        {
            part[i] = GatlingArray[i].transform.GetChild(0).GetComponent<ParticleSystem>();
            MuzzleFlash[i] = GatlingArray[i].transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
            WhiteMuzzleFlash[i] = GatlingArray[i].transform.GetChild(1).GetChild(1).GetComponent<ParticleSystem>();
        }
        foreach(ParticleSystem particles in part)
        {
            particles.enableEmission = false;
        }
        foreach (ParticleSystem particles in MuzzleFlash)
        {
            particles.enableEmission = false;
        }
        foreach (ParticleSystem particles in WhiteMuzzleFlash)
        {
            particles.enableEmission = false;
        }
        

        rightTriggerAction = rightHandTrigger.ToInputAction();
    }
    private void Update()
    {
        if(Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }

        //bool inputFire = rightTriggerAction.IsPressed();
        bool inputFire = false;

        if (Input.GetButton("Fire"))
        {
            inputFire = true;
        }
        //anim.SetBool("Firing", inputFire && GameFeelManager.instance.FireGF);
        if (furyBarScript)
        {
            if(Input.GetKeyDown(KeyCode.L) && furyBarScript.FurySlider.value >= furyBarScript.FurySlider.maxValue)
            {
                //AHAH big lazer go BRRRRRRR
                
                StartCoroutine(BigLazerGoBRRR());
            }
        }



        if (inputFire)
        {
            if(GameFeelManager.instance.ShakeGF)
                impulseSource.GenerateImpulse();

            if (GameFeelManager.instance.FireGF && !LazerGO.activeInHierarchy)
            {
                Debug.Log("Gatling");
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
                Debug.Log("Basic");
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
        foreach (ParticleSystem particles in part)
        {
            particles.enableEmission = on;
        }
        foreach (ParticleSystem particles in MuzzleFlash)
        {
            particles.enableEmission = on;
        }
        foreach (ParticleSystem particles in WhiteMuzzleFlash)
        {
            particles.enableEmission = on;
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
