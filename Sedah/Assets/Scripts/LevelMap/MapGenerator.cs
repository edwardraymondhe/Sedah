using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GridVisualizer gridVisualizer;
    public MapVisualizer mapVisualizer;
    public Direction startEdge, exitEdge;

    public int numberOfPieces;

    public bool visualizeUsingPrefabs = false;
    public bool autoRepair = true;
    public bool randomPlacement;
    private Vector3 startPosition, exitPosition;
    
    [Range(3, 20)]
    public int width, length = 11;
    private MapGrid grid;
    CandidateMap map;

    private int enemyCount;
    
    public void SetParam(int mapComplexity, int gridWidth, int gridLength, int enemyCount)
    {
        numberOfPieces = mapComplexity;
        width = gridWidth;
        length = gridLength;

        this.enemyCount = enemyCount;
    }

    public void Initialization()
    {
        gridVisualizer.VisualizeGrid(width, length);

        GenerateNewMap();

        transform.parent.Rotate(Vector3.up, 45);
    }

    public void GenerateNewMap()
    {
        // Clears everything in our dictionary and map
        mapVisualizer.ClearMap();

        // Creates a new grid
        grid = new MapGrid(width, length);

        // Selects points
        MapHelper.RandomlyChooseAndSetStartAndExit(grid, ref startPosition, ref exitPosition, randomPlacement, startEdge, exitEdge);

        // Creates a map and visualizes it
        map = new CandidateMap(grid, numberOfPieces, enemyCount);
        map.CreateMap(startPosition, exitPosition, enemyCount, autoRepair);
        mapVisualizer.VisualizeMap(grid, map.ReturnMapData(), visualizeUsingPrefabs);
    }

    public void TryRepair()
    {
        if(map != null)
        {
            var listOfObstaclesToRemove = map.Repair();
            if(listOfObstaclesToRemove.Count > 0)
            {
                mapVisualizer.ClearMap();
                mapVisualizer.VisualizeMap(grid, map.ReturnMapData(), visualizeUsingPrefabs);
            }
        }
    }
}
