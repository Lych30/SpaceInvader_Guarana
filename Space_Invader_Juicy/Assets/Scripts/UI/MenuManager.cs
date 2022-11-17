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

    void DesactivateMenu()
    {
        victoryScreen.SetActive(false);
        losingScreen.SetActive(false);
    }

    public void ActivateVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }

    public void ActivateLosingScreen()
    {
        losingScreen.SetActive(true);
    }

    public void UI_MainMenu(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;
        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {
            buttonToPunch.GetComponent<Button>().enabled = true;
            Time.timeScale = 1;
            SceneManager.LoadScene(0); // 0 = MainMenu Scene
        });
    }

    public void UI_StartGame(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;

        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {
            buttonToPunch.GetComponent<Button>().enabled = true;

            Time.timeScale = 1;
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

    public void UI_RestartLevel(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<Button>().enabled = false;

        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {
            buttonToPunch.GetComponent<Button>().enabled = true;

            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

    }

}
