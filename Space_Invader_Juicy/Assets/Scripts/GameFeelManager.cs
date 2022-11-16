using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GameFeelManager : MonoBehaviour
{
    public bool FireGF;
    public bool ShakeGF;
    public bool PostProcessGF;
    public bool EnnemyExplosionGF;


    public Volume Volume;
    // Update is called once per frame
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
                default:
                    Debug.Log("none");
                    break;
            }
        }
    }
}
