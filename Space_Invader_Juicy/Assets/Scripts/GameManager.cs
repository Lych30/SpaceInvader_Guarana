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
    }

    private void OnDestroy()
    {
        EnemyKilled.RemoveAllListeners();
    }

    public void Victory()
    {
    }

    public void Defeat()
    {
    }

    public void ReloadScene()
    {
    }

    private void IncrementEnemyCount() => enemyCount++;
    private void DecrementEnemyCount() => enemyCount--;

    private void CheckForEnemyLeft()
    {
        if (enemyCount <= 0)
            Victory();
    }
}
