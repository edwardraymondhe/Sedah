using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Responsible for placing obstacles, calculating path, fixing path
public class CandidateMap
{
    private MapGrid grid;
    private int numberOfPieces = 0;
    private int enemyCount = 0;
    private bool[] obstaclesArray = null;
    private Vector3 startPoint, exitPoint;
    private List<KnightPiece> knightPiecesList;
    private List<Vector3> path = new List<Vector3>();

    public MapGrid Grid { get => grid;}
    public bool[] ObstaclesArray { get => obstaclesArray;}

    public CandidateMap(MapGrid grid, int numberOfPieces, int enemyCount)
    {
        this.numberOfPieces = numberOfPieces;
        this.knightPiecesList = new List<KnightPiece>();
        this.obstaclesArray = new bool[grid.Width * grid.Length];
        this.grid = grid;
        this.enemyCount = enemyCount;
    }

    public void CreateMap(Vector3 startPosition, Vector3 exitPosition, int enemyCount, bool autoRepair = false)
    {
        this.startPoint = startPosition;
        this.exitPoint = exitPosition;
        this.obstaclesArray = new bool[grid.Width * grid.Length];
        this.knightPiecesList = new List<KnightPiece>();
        this.enemyCount = enemyCount;
        RandomlyPlaceKnightPieces(this.numberOfPieces);
        
        PlaceObstacles();
        FindPath();

        if(autoRepair)
        {
            Repair();
        }
    }

    private void FindPath()
    {
        this.path = Astar.GetPath(startPoint, exitPoint, obstaclesArray, grid);
    }

    private bool CheckIfPositionCanBeObstacle(Vector3 position)
    {
        if(position == startPoint || position == exitPoint)
            return false;
        
        int index = grid.CalculateIndexFromCoordinates(position.x, position.z);

        return obstaclesArray[index] == false;
    }

    private void RandomlyPlaceKnightPieces(int numberOfPieces)
    {
        var count = numberOfPieces;
        var knightPlacementTryLimits = 1000;
        Debug.Log(obstaclesArray.Length);
        // Limits loop iteration
        while(count > 0 && knightPlacementTryLimits > 0)
        {
            var randomIndex = Random.Range(0, obstaclesArray.Length);

            // If current position hasn't placed a knight before
            if(obstaclesArray[randomIndex] == false)
            {

                // Generates a coordinate and compare it with invalid positions
                var coordinates = grid.CalculateCoordinatesFromIndex(randomIndex);
                if(coordinates == startPoint || coordinates == exitPoint)
                    continue;
                
                // Sets the position as true, indicated that it's been placed with a knight
                obstaclesArray[randomIndex] = true;
                knightPiecesList.Add(new KnightPiece(coordinates));

                --count;
            }

            --knightPlacementTryLimits;
        }
    }

    public MapData ReturnMapData()
    {
        return new MapData
        {
            obstacleArray = this.obstaclesArray,
            knightPiecesList = knightPiecesList,
            enemyCount = this.enemyCount,
            startPosition = startPoint,
            exitPosition = exitPoint,
            path = this.path,
            environmentType = (EnvironmentType)Random.Range((int)EnvironmentType.Dungeon, (int)EnvironmentType.Mountain)
        };
    }

    public List<Vector3> Repair()
    {
        int numberOfObstacles = obstaclesArray.Where(obstacle => obstacle == true).Count();
        List<Vector3> listOfObstaclesToRemove = new List<Vector3>();
        if(path.Count <= 0)
        {
            do
            {
                int obstacleIndexToRemove = Random.Range(0, numberOfObstacles);
                for(int i = 0; i < obstaclesArray.Length; i++)
                {
                    if(obstaclesArray[i])
                    {
                        if(obstacleIndexToRemove == 0)
                        {
                            obstaclesArray[i] = false;
                            listOfObstaclesToRemove.Add(grid.CalculateCoordinatesFromIndex(i));
                            break;
                        }
                        obstacleIndexToRemove--;
                    }
                }

                FindPath();
            }while(this.path.Count <= 0);
        }

        foreach (var obstaclePosition in listOfObstaclesToRemove)
        {
            if(path.Contains(obstaclePosition) == false)
            {
                int index = grid.CalculateIndexFromCoordinates(obstaclePosition.x, obstaclePosition.z);
                obstaclesArray[index] = true;
            }
        }

        return listOfObstaclesToRemove;
    }

    private void PlaceObstaclesAroundKnight(KnightPiece knight)
    {
        foreach (var position in KnightPiece.listOfPossibleMoves)
        {
            // Potential position of obstacle
            var newPosition = knight.Position + position;
            if(grid.IsCellValid(newPosition.x, newPosition.z) && CheckIfPositionCanBeObstacle(newPosition))
            {
                obstaclesArray[grid.CalculateIndexFromCoordinates(newPosition.x, newPosition.z)] = true;
            }
        }
    }

    private void PlaceObstacles()
    {
        foreach (var knight in knightPiecesList)
        {
            PlaceObstaclesAroundKnight(knight);
        }
    }
}
