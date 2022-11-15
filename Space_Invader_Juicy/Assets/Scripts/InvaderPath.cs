using System;
using System.Collections.Generic;
using UnityEngine;

public class InvaderPath : MonoBehaviour
{
    [SerializeField] private List<Transform> points;

    public (Vector3 position, int index) GetNextPosition(int currentIndex)
    {
        if(points.Count == 0) throw new IndexOutOfRangeException("No Points In InvaderPath");

        if (currentIndex == -1 || currentIndex >= points.Count - 1) return (Vector3.zero, -1);

        currentIndex++;
        return (points[currentIndex].position, currentIndex);
    }
}
