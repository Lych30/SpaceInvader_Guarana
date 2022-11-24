using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderPath : MonoBehaviour
{
    [Serializable] public struct EnemyLane
    {
        [Min(0)] public int index;
        public GameObject enemyPrefab;
        public int enemyCount;

        [Header("   Spawn Position")]
        public float offset;
        public float initialOffset;

        [Header("   Delay")]
        [Min(0)] public int spawnAfterLane;

        [HideInInspector] public Mesh mesh;
        [HideInInspector] public Quaternion rotation;
    }

    [Header("Path Numbers")]
    [SerializeField] private int laneNumber = 6;
    [SerializeField] private Vector2 laneSize = new Vector2(10f, 3f);

    [Header("Enemies")]
    [SerializeField] private Transform enemyParentTransform;
    [SerializeField] private List<EnemyLane> enemyLanes;

    private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        GeneratePoints();
        SpawnInvaders();
    }

    public (Vector3 position, int index) GetNextPosition(int currentIndex)
    {
        if (positions.Count == 0) throw new IndexOutOfRangeException("No Points In InvaderPath");
        
        if (currentIndex == -1 || currentIndex >= positions.Count - 1)
        {
            return (Vector3.zero, -1);
        }

        currentIndex++;

        return (positions[currentIndex], currentIndex);
    }

    private void GeneratePoints()
    {
        Vector3 pos = transform.position;
        Vector3 farPos = new Vector3(pos.x + laneSize.x, pos.y, pos.z);
        for (int i = 0; i < laneNumber; ++i, pos.y -= laneSize.y, farPos.y -= laneSize.y)
        {
            positions.Add(pos);
            positions.Insert(i % 2 == 0 ? positions.Count : positions.Count - 1, farPos);
        }
    }

    private void SpawnInvaders()
    {
        enemyParentTransform.position = transform.position;
        for (int i = 0; i < enemyLanes.Count; ++i)
        {
            if (enemyLanes[i].spawnAfterLane == 0)
                SpawnLane(i);
            else
                StartCoroutine(DelaySpawn(i));
        }
    }

    private void SpawnLane(int index)
    {
        Vector3 pos = new Vector3(
            enemyParentTransform.position.x + enemyLanes[index].initialOffset,
            enemyParentTransform.position.y - laneSize.y * index,
            enemyParentTransform.position.z);
        int n;

        for (n = 0; n < enemyLanes[index].enemyCount; ++n, pos.x += enemyLanes[index].offset)
            Instantiate(enemyLanes[index].enemyPrefab, pos, Quaternion.identity, enemyParentTransform);
    }

    private IEnumerator DelaySpawn(int index)
    {
        var lane = enemyLanes[index];
        float limitY = enemyParentTransform.position.y - lane.spawnAfterLane * laneSize.y;

        yield return new WaitUntil(() => enemyParentTransform.position.y <= limitY);

        SpawnLane(index);
    }

    private void OnDrawGizmos()
    {
        // Draw Path preview
        Gizmos.color = Color.green;
        Vector3 pos = transform.position;
        for (int i = 0; i < laneNumber; ++i, pos.y -= laneSize.y)
        {
            Vector3 farPos = new Vector3(pos.x + laneSize.x, pos.y, pos.z);
            Gizmos.DrawWireSphere(pos, .1f);
            Gizmos.DrawWireSphere(farPos, .1f);
            Gizmos.DrawLine(pos, farPos);
            if (i == laneNumber - 1)
                continue;
            Gizmos.DrawLine(i % 2 != 0 ? pos : farPos, i % 2 != 0 ?
                new Vector3(pos.x, pos.y - laneSize.y, pos.z) :
                new Vector3(farPos.x, farPos.y - laneSize.y, farPos.z));
        }

        if (UnityEditor.EditorApplication.isPlaying)
            return;

        Gizmos.color = Color.red;
        SkinnedMeshRenderer skin; Mesh tempMesh; EnemyLane lane; Vector3 enemyPos;
        pos = transform.position;
        for (int i = 0; i < enemyLanes.Count; ++i)
        {
            lane = enemyLanes[i];

            // Get Meshs
            if (lane.enemyPrefab == null && lane.mesh == null)
                continue;

            try
            {
                skin = lane.enemyPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
                tempMesh = skin?.sharedMesh;

                if (skin == null && lane.mesh != null)
                {
                    lane.mesh = null;
                    enemyLanes[i] = lane;
                    continue;
                }
                else if (tempMesh != lane.mesh)
                {
                    lane.rotation = skin.transform.rotation;
                    lane.mesh = tempMesh;
                    enemyLanes[i] = lane;
                }
            }
            catch
            {
                continue;
            }

            // Draw Invaders
            for (int n = 0; n < lane.enemyCount; ++n)
            {
                enemyPos = new Vector3(
                    pos.x + lane.initialOffset + n * lane.offset,
                    pos.y - lane.index * laneSize.y,
                    pos.z);
                Gizmos.DrawMesh(lane.mesh, enemyPos, lane.rotation, Vector3.one * 1.75f);
            }
        }
    }
}
