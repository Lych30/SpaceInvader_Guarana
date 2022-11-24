using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public bool canPause;
    bool isPause;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseMenuVR;

    [SerializeField] List<GameObject> vrLaser;

    public Vector3 tweenPunchScale = new Vector3(0.2f, 0.2f, 0.2f);
    public float duration = 0.35f;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UI_Pause();
        }
    }

    public void UI_Pause()
    {
        if (!canPause)
        {
            return;
        }

        isPause = !isPause;

        if (isPause)
        {
            pauseMenu.SetActive(true);
            pauseMenuVR?.SetActive(true);
            Time.timeScale = 0;

            foreach (var item in vrLaser)
            {
                item.SetActive(true);
            }
        }
        else
        {
            pauseMenu.SetActive(false);
            pauseMenuVR?.SetActive(false);
            Time.timeScale = 1;

            foreach (var item in vrLaser)
            {
                item.SetActive(false);
            }
        }
    }

    public void UI_PauseButton(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;

        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {

            buttonToPunch.GetComponent<Button>().enabled = true;

            isPause = !isPause;

            if (isPause)
            {
                pauseMenu.SetActive(true);
                pauseMenuVR?.SetActive(true);
                Time.timeScale = 0;

                foreach (var item in vrLaser)
                {
                    item.SetActive(true);
                }
            }
            else
            {
                pauseMenu.SetActive(false);
                pauseMenuVR?.SetActive(false);
                Time.timeScale = 1;

                foreach (var item in vrLaser)
                {
                    item.SetActive(false);
                }
            }
        });

        
    }

    public void ShowPlayerRays()
    {
        foreach (var item in vrLaser)
        {
            item.SetActive(true);
        }
    }

    public void HidePlayerRays()
    {
        foreach (var item in vrLaser)
        {
            item.SetActive(false);
        }
    }
}
