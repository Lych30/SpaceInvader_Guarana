using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GameFeelManager : MonoBehaviour
{
    public static GameFeelManager instance;
    public bool FireGF;
    public bool ShakeGF;
    public bool PostProcessGF;
    public bool EnnemyExplosionGF;
    public bool EnnemyAnimGF;
    public bool PopScoreGF;
    public bool BackgroundParticlesGF;
    public bool LazerGF;
    public bool ImpulseGF;

    public Volume Volume;
    public GameObject BGParticles;
    public GameObject FuryBarScriptGO;
    public GameObject FuryBarScriptGO2;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }
   

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && Input.anyKeyDown)
        {
            switch (e.keyCode)
            {
                case KeyCode.Alpha1:
                    FireGF = !FireGF;
                    break;
                case KeyCode.Alpha2:
                    ShakeGF = !ShakeGF;
                    break;
                case KeyCode.Alpha3:

                    PostProcessGF = !PostProcessGF;

                    if (PostProcessGF)
                    {
                        Volume.enabled = true;
                    }
                    else
                    {
                        Volume.enabled = false;
                    }

                    break;

                case KeyCode.Alpha4:
                    EnnemyExplosionGF = !EnnemyExplosionGF;
                    break;
                case KeyCode.Alpha5:
                    EnnemyAnimGF = !EnnemyAnimGF;
                    break;
                case KeyCode.Alpha6:
                    PopScoreGF = !PopScoreGF;
                    break;
                case KeyCode.Alpha7:
                    BackgroundParticlesGF = !BackgroundParticlesGF;
                    if (BackgroundParticlesGF)
                    {

                        BGParticles.SetActive(true);
                    }
                    else
                    {
                        BGParticles.SetActive(false);
                    }
                    break;
                case KeyCode.Alpha8:
                    LazerGF = !LazerGF;
                    FuryBarScriptGO.SetActive(LazerGF);
                    FuryBarScriptGO2.SetActive(LazerGF);
                    break;

                case KeyCode.Alpha9:
                    ImpulseGF = !ImpulseGF;
                    break;

                case KeyCode.Slash:
                    //ACTIVATE ALL EFFECTS
                    FireGF = true;

                    ShakeGF = true;

                    PostProcessGF = true;

                    EnnemyExplosionGF = true;

                    EnnemyAnimGF = true;

                    PopScoreGF = true;

                    Volume.enabled = true;

                    BackgroundParticlesGF = true;

                    BGParticles.SetActive(true);

                    LazerGF = true;

                    FuryBarScriptGO.SetActive(true);

                    FuryBarScriptGO2.SetActive(true);

                    ImpulseGF = true;

                    break;

                    case KeyCode.Exclaim:

                    //DEACTIVATE ALL EFFECTS
                    FireGF = false;

                    ShakeGF = false;

                    PostProcessGF = false;

                    EnnemyExplosionGF = false;

                    EnnemyAnimGF = false;

                    PopScoreGF = false;

                    Volume.enabled = false;

                    BackgroundParticlesGF = false;

                    BGParticles.SetActive(false);

                    LazerGF = false;

                    FuryBarScriptGO.SetActive(false);

                    FuryBarScriptGO2.SetActive(false);

                    ImpulseGF = false;
                    break;
                default:
                    Debug.Log("none");
                    break;
            }
        }
    }
}
