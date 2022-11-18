using System;
using System.Collections.Generic;
using UnityEngine;

public class InvaderPath : MonoBehaviour
{
    [Serializable] public enum MovementMethod { LerpTo, TeleportTo }
    [Serializable] public enum Direction { Horizontal, Vertical }
    [Serializable] public struct PathPoint
    {
        [SerializeField] private Transform point;
        [SerializeField] private MovementMethod method;
        [SerializeField] private Direction direction;

        public Vector3 Position { get { return point.position; } }
        public bool LerpTo { get { return method == MovementMethod.LerpTo; } }
        public bool MoveDown { get { return direction == Direction.Vertical; } }
    }

    [SerializeField] private List<PathPoint> pathPoints;
    [SerializeField] private bool UseTeleportOption = false;

    public (Vector3 position, int index, bool moveDown) GetNextPosition(int currentIndex, out bool lerpToNextPosition)
    {
        if(pathPoints.Count == 0) throw new IndexOutOfRangeException("No Points In InvaderPath");
        
        if (currentIndex == -1 || currentIndex >= pathPoints.Count - 1)
        {
            lerpToNextPosition = true;
            return (Vector3.zero, -1, true);
        }


        currentIndex++;
        lerpToNextPosition = !UseTeleportOption || pathPoints[currentIndex].LerpTo;
        
        return (pathPoints[currentIndex].Position, currentIndex, pathPoints[currentIndex].MoveDown);
    }
}
