using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [HideInInspector] public UnityEvent EnemyKilled = new UnityEvent();
    [HideInInspector] public UnityEvent InvaderCountIncrement = new UnityEvent();

    private int enemyCount = 0;
    private List<Invader> enemyList = new List<Invader>();

    private float timeBeforeNextShoot = 3;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        EnemyKilled.AddListener(DecrementEnemyCount);
        EnemyKilled.AddListener(CheckForEnemyLeft);
    }

    void Update()
    {
        // Effect Toggle Inputs


        //Random for enemy shoot
        if (timeBeforeNextShoot > 0)
        {
            timeBeforeNextShoot -= Time.deltaTime;
        }
        else
        {
            if (enemyList.Count < 5)
                timeBeforeNextShoot = Random.Range(0f, 3f);
            else
                timeBeforeNextShoot = Random.Range(0f, enemyList.Count);

            EnemyShoot();
        }
    }

    private void OnDestroy()
    {
        EnemyKilled.RemoveAllListeners();
    }

    public void Victory()
    {
        Debug.Log("You won !");
    }

    public void Defeat()
    {
        Debug.Log("You lost !");
    }

    public void ReloadScene()
    {
    }

    public void EnemyShoot()
    {
        int ran = Random.Range(0, enemyList.Count);
        enemyList[ran].Shoot();
    }

    private void IncrementEnemyCount() => enemyCount++;
    private void DecrementEnemyCount() => enemyCount--;

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
