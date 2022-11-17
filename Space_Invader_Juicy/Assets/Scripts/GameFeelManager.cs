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

    public Volume Volume;
    public GameObject BGParticles;

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
                default:
                    Debug.Log("none");
                    break;
            }
        }
    }
}
