using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private List<Invader> enemyList = new List<Invader>();

    private float timeBeforeNextShoot = 3;

    [SerializeField] MenuManager refMenuManager;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Effect Toggle Inputs


        //Random for enemy shoot
        if (timeBeforeNextShoot > 0)
        {
            timeBeforeNextShoot -= Time.deltaTime;
        }
        else if (enemyList.Count > 0)
        {
            if (enemyList.Count < 5)
                timeBeforeNextShoot = Random.Range(0f, 3f);
            else
                timeBeforeNextShoot = Random.Range(0f, enemyList.Count);

            EnemyShoot();
        }
    }

    public void OnLevelWasLoaded()
    {
        refMenuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }

    public void EnemyShoot()
    {
        int ran = Random.Range(0, enemyList.Count);
        enemyList[ran].Shoot();
    }

    public void Victory()
    {
        refMenuManager.ActivateVictoryScreen();
    }

    public void Defeat()
    {
        refMenuManager.ActivateLosingScreen();
        Time.timeScale = 0;
    }

    public void ReloadScene()
    {
    }

    public void IncrementEnemy(Invader enemy)
    {
        enemyList.Add(enemy);
    }

    public void DecrementEnemy(Invader enemy)
    {
        enemyList.Remove(enemy);
        CheckForEnemyLeft();
    }

    private void CheckForEnemyLeft()
    {
        if (enemyList.Count <= 0)
            Victory();
    }
}
