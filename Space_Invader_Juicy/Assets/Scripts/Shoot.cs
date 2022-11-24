using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{

    public Gradient epilepsyWarning;
    Volume volume;
    
    public GameObject[] GatlingArray;
    ParticleSystem[] part;
    ParticleSystem[] MuzzleFlash;
    ParticleSystem[] WhiteMuzzleFlash;
    public GameObject[] GatlingArrayVR;
    [SerializeField]ParticleSystem[] partVR;
    [SerializeField] ParticleSystem[] MuzzleFlashVR;
    [SerializeField] ParticleSystem[] WhiteMuzzleFlashVR;
    CinemachineImpulseSource impulseSource;
    public GameObject StandarBullet;
    public AudioSource ShootSound_SpaceInvaders;
    public AudioSource GatlingSound;
    public AudioSource LazerSound;
    private float TimerBtwShots = 1.0f;
    private float Timer;
    public FuryBarScript VRfuryBarScript;
    public FuryBarScript MainfuryBarScript;
    public GameObject LazerGO;
    CinemachineImpulseSource LazerShake;

    [Range(1.0f, 4.0f)]
    public float DecreaseSpeedFactor;
    Controler ctrl;

    [SerializeField] ActionBasedController xrLeft;
    [SerializeField] ActionBasedController xrRight;
    [SerializeField] float impulseMagnitude = 0.7f;
    [SerializeField] float impulseDuration = 0.1f;

    [SerializeField] private Animator anim;

    void Start()
    {
        volume = FindObjectOfType<Volume>();
        Timer = TimerBtwShots;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        LazerShake = LazerGO.GetComponent<CinemachineImpulseSource>();

        //Arrays Init
        part = new ParticleSystem[GatlingArray.Length];
        MuzzleFlash = new ParticleSystem[GatlingArray.Length];
        WhiteMuzzleFlash = new ParticleSystem[GatlingArray.Length];

        partVR = new ParticleSystem[GatlingArrayVR.Length];
        MuzzleFlashVR = new ParticleSystem[GatlingArrayVR.Length];
        WhiteMuzzleFlashVR = new ParticleSystem[GatlingArrayVR.Length];

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

        for (int i = 0; i < GatlingArrayVR.Length; i++)
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


        ctrl = FindObjectOfType<Controler>();
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

        if (GameFeelManager.instance.LazerGF)
        {
            if((Input.GetKeyDown(KeyCode.L) || (VRInputManager.Instance.LeftStick.Direction.y < -0.8f && VRInputManager.Instance.RightStick.Direction.y < -0.8f)) && VRfuryBarScript.FurySlider.value >= VRfuryBarScript.FurySlider.maxValue)
            {
                //AHAH big lazer go BRRRRRRR
                AudioSource[] everyAudioSource = FindObjectsOfType<AudioSource>();
                foreach(var audio in everyAudioSource)
                {
                    audio.Stop();
                }

                if(Time.timeScale != 0)
                    LazerSound.Play();

                StartCoroutine(BigLazerGoBRRR());
            }
        }

        anim.SetBool("FiringLeft", (inputFire || inputFireLeft) && GameFeelManager.instance.FireGF);
        anim.SetBool("FiringRight", (inputFire || inputFireRight) && GameFeelManager.instance.FireGF);

        if (inputFire || inputFireLeft || inputFireRight)
        {
            if(GameFeelManager.instance.ShakeGF)
                impulseSource.GenerateImpulse();

            if (GameFeelManager.instance.FireGF && !LazerGO.activeInHierarchy)
            {
                //UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand.Create(0, 5, 1);
                if ((inputFireLeft || inputFireRight) && GatlingArrayVR.Length == 2)
                {
                    GatlingParticlesVR(inputFireLeft, inputFireRight);
                }    
                else if(inputFire && GatlingArray.Length > 0)
                {
                    GatlingParticlesMouse(true);
                }

                if (GameFeelManager.instance.LazerGF)
                {
                    VRfuryBarScript.AddFury(1);
                    MainfuryBarScript.AddFury(1);
                }


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

        if (Left)
            ActivateHapticLeft();

        if (Right)
            ActivateHapticRight();
    }

    void ActivateHapticLeft()
    {
        xrLeft.SendHapticImpulse(impulseMagnitude, impulseDuration);
    }

    void ActivateHapticRight()
    {
        xrRight.SendHapticImpulse(impulseMagnitude, impulseDuration);
    }

    IEnumerator BigLazerGoBRRR()
    {
        LazerGO.SetActive(true);
        ctrl.speed /= DecreaseSpeedFactor;
        Bloom bloom;
        while (VRfuryBarScript.FurySlider.value > 0)
        {
            LazerShake.GenerateImpulse();
            VRfuryBarScript.AddFury(-10);
            MainfuryBarScript.AddFury(-10);
            if (volume.profile.TryGet<Bloom>(out bloom))
            {
                bloom.tint.overrideState = true;
                //La version soft
                bloom.tint.value = epilepsyWarning.Evaluate(VRfuryBarScript.FurySlider.value / VRfuryBarScript.FurySlider.maxValue);
                // La version de l'épilepsie
                //bloom.tint.value = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255),255);
          
                
            }
            

            yield return new WaitForFixedUpdate();
        }
        //Return to normal if necessary
        if (volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.tint.overrideState = true;
            bloom.tint.value = Color.red;
        }
        ctrl.speed *= DecreaseSpeedFactor;
        LazerGO.SetActive(false);
    }
}
