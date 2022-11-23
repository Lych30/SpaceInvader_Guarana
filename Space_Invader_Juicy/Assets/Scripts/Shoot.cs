using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Shoot : MonoBehaviour
{

    public Gradient epilepsyWarning;
    Volume volume;
    

    public GameObject[] GatlingArray;
    ParticleSystem[] part;
    ParticleSystem[] MuzzleFlash;
    ParticleSystem[] WhiteMuzzleFlash;
    public GameObject[] GatlingArrayVR;
    ParticleSystem[] partVR;
    ParticleSystem[] MuzzleFlashVR;
    ParticleSystem[] WhiteMuzzleFlashVR;
    CinemachineImpulseSource impulseSource;
    public GameObject StandarBullet;
    public AudioSource ShootSound_SpaceInvaders;
    public AudioSource GatlingSound;
    private float TimerBtwShots = 1.0f;
    private float Timer;
    public FuryBarScript furyBarScript;
    public GameObject LazerGO;
    CinemachineImpulseSource LazerShake;


    //[SerializeField] private Animator anim;

    void Start()
    {
        volume = Camera.main.GetComponent<Volume>();

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

        for (int i = 0; i < GatlingArray.Length; i++)
        {
            partVR[i] = GatlingArrayVR[i].transform.GetChild(0).GetComponent<ParticleSystem>();
            MuzzleFlashVR[i] = GatlingArrayVR[i].transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
            WhiteMuzzleFlashVR[i] = GatlingArrayVR[i].transform.GetChild(1).GetChild(1).GetComponent<ParticleSystem>();
        }
        foreach (ParticleSystem particles in partVR)
        {
            particles.enableEmission = false;
        }
        foreach (ParticleSystem particles in MuzzleFlashVR)
        {
            particles.enableEmission = false;
        }
        foreach (ParticleSystem particles in WhiteMuzzleFlashVR)
        {
            particles.enableEmission = false;
        }



    }
    private void Update()
    {
        if(Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }

        //bool inputFire = rightTriggerAction.IsPressed();
        bool inputFire = false;
        bool inputFireLeft = VRInputManager.Instance.LeftTrigger.GetButton;
        bool inputFireRight = VRInputManager.Instance.RightTrigger.GetButton;

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



        if (inputFire || inputFireLeft || inputFireRight)
        {
            if(GameFeelManager.instance.ShakeGF)
                impulseSource.GenerateImpulse();

            if (GameFeelManager.instance.FireGF && !LazerGO.activeInHierarchy)
            {
                if(inputFire && GatlingArray.Length > 0)
                    GatlingParticlesMouse(true);

                if ((inputFireLeft || inputFireRight) && GatlingArrayVR.Length == 2)
                    GatlingParticlesVR(inputFireLeft, inputFireRight);

                if (furyBarScript)
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
                GatlingParticlesMouse(false);
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
            GatlingParticlesMouse(false);
            GatlingParticlesVR(false, false);
            if (GatlingSound.isPlaying)
            {
                GatlingSound.Stop();
            }
        }
        
    }

    private void GatlingParticlesMouse(bool on)
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

    private void GatlingParticlesVR(bool Left, bool Right)
    {
        if(partVR.Length == 2)
        {
            partVR[0].enableEmission = Left;
            partVR[1].enableEmission = Right;
        }
        if(MuzzleFlashVR.Length == 2)
        {
            MuzzleFlashVR[0].enableEmission = Left;
            MuzzleFlashVR[1].enableEmission = Right;
        }
        if(WhiteMuzzleFlashVR.Length == 2)
        {
            WhiteMuzzleFlashVR[0].enableEmission = Left;
            WhiteMuzzleFlashVR[1].enableEmission = Right;
        }
    }



    IEnumerator BigLazerGoBRRR()
    {
        LazerGO.SetActive(true);

        Bloom bloom;
        while (furyBarScript.FurySlider.value > 0)
        {
            LazerShake.GenerateImpulse();
            furyBarScript.AddFury(-10);
            
            if (volume.profile.TryGet<Bloom>(out bloom))
            {
                bloom.tint.overrideState = true;
                //La version soft
                bloom.tint.value = epilepsyWarning.Evaluate(furyBarScript.FurySlider.value / furyBarScript.FurySlider.maxValue);
                // La version de l'épilepsie
                //bloom.tint.value = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255),255);
          
                
                Debug.Log(furyBarScript.FurySlider.value / furyBarScript.FurySlider.maxValue);
                
            }
            

            yield return new WaitForFixedUpdate();
        }
        //Return to normal if necessary
        if (volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.tint.overrideState = true;
            bloom.tint.value = Color.red;
        }
        
        LazerGO.SetActive(false);
    }
}
