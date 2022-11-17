using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    SoundMenu instance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonClick;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnLevelWasLoaded()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void UI_ButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
}
