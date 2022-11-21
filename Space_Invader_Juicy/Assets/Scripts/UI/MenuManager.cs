using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Vector3 tweenPunchScale = new Vector3(0.2f, 0.2f, 0.2f);
    public float duration = 0.35f;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject losingScreen;

    [SerializeField] PauseManager refPauseManager;

    void DesactivateMenu()
    {
        pauseMenu.SetActive(false);
        victoryScreen.SetActive(false);
        losingScreen.SetActive(false);
    }

    public void ActivateVictoryScreen()
    {
        pauseMenu.SetActive(false);
        victoryScreen.SetActive(true);
        refPauseManager.canPause = false;
    }

    public void ActivateLosingScreen()
    {
        pauseMenu.SetActive(false);
        losingScreen.SetActive(true);
        refPauseManager.canPause = false;
    }

    public void UI_MainMenu(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;
        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1;
            refPauseManager.canPause = true;
            buttonToPunch.GetComponent<Button>().enabled = true;
            SceneManager.LoadScene(0); // 0 = MainMenu Scene
        });
    }

    public void UI_RestartLevel(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;

        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {

            Time.timeScale = 1;
            refPauseManager.canPause = true;
            buttonToPunch.GetComponent<Button>().enabled = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

    }

    public void UI_StartGame(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;

        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1;
            if (refPauseManager != null)
            {
                refPauseManager.canPause = true;
            }
            buttonToPunch.GetComponent<Button>().enabled = true;
            SceneManager.LoadScene(1); // 1 = Game Scene 
        });
        
    }

    public void UI_Exit(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;

        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {
            buttonToPunch.GetComponent<Button>().enabled = true;

            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        });
        
    }

    

}
