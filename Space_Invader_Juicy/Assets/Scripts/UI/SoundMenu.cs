using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonClick;

    public void UI_ButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
}
