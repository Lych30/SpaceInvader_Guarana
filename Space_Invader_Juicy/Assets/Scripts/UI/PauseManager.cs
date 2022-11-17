using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    bool isPause;
    [SerializeField] GameObject pauseMenu;

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
        isPause = !isPause;

        if (isPause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
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
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        });

        
    }


}
