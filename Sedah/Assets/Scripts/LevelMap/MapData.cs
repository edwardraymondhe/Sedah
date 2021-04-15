using System.Collections.Generic;
using UnityEngine;

public struct MapData
{
    public bool[] obstacleArray;
    public List<KnightPiece> knightPiecesList;
    public int enemyCount;
    public Vector3 startPosition;
    public Vector3 exitPosition;
    public List<Vector3> path;
    public EnvironmentType environmentType;
}
