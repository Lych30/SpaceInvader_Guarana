using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Vector3 tweenPunchScale = new Vector3(0.2f, 0.2f, 0.2f);
    public float duration = 0.35f;

    public void UI_MainMenu(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).OnComplete(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0); // 0 = MainMenu Scene
        });

    }

    public void UI_StartGame(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).OnComplete(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1); // 1 = Game Scene 
        });
        
    }

    public void UI_Exit(GameObject buttonToPunch)
    {
        buttonToPunch.GetComponent<RectTransform>().transform.DOPunchScale(tweenPunchScale, duration).SetUpdate(true).OnComplete(() =>
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        });
        
    }

}
