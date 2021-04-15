using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Astar
{
    public static List<Vector3> GetPath(Vector3 start, Vector3 exit, bool[] obstaclesArray, MapGrid grid)
    {
        VertexPosition startVertex = new VertexPosition(start);
        VertexPosition exitVertex = new VertexPosition(exit);

        List<Vector3> path = new List<Vector3>();

        List<VertexPosition> openedList = new List<VertexPosition>();
        HashSet<VertexPosition> closedList = new HashSet<VertexPosition>();

        startVertex.estimatedCost = ManhattanDistance(startVertex, exitVertex);

        openedList.Add(startVertex);

        VertexPosition currentVertex = null;

        while(openedList.Count > 0)
        {
            openedList.Sort();
            currentVertex = openedList[0];

            if(currentVertex.Equals(exitVertex))
            {
                while(currentVertex != startVertex)
                {
                    path.Add(currentVertex.Position);
                    currentVertex = currentVertex.previousVertex;
                }
                path.Reverse();
                break;
            }

            var arrayOfNeighbors = FindNeighborsForVertexPosition(currentVertex, grid, obstaclesArray);
            foreach (var neighbor in arrayOfNeighbors)
            {
                if(neighbor == null || closedList.Contains(neighbor))
                {
                    continue;
                }

                // Don't take obstacle
                if(neighbor.IsTaken == false)
                {
                    var totalCost = currentVertex.totalCost + 1;
                    var neighborEstimatedCost = ManhattanDistance(neighbor, exitVertex);
                    neighbor.totalCost = totalCost;
                    neighbor.previousVertex = currentVertex;
                    neighbor.estimatedCost = totalCost + neighborEstimatedCost;

                    if(openedList.Contains(neighbor) == false)
                    {
                        openedList.Add(neighbor);
                    }
                }
            }
            closedList.Add(currentVertex);
            openedList.Remove(currentVertex);
        }

        return path;
    }

    private static VertexPosition[] FindNeighborsForVertexPosition(VertexPosition currentVertex, MapGrid grid, bool[] obstaclesArray)
    {
        VertexPosition[] arrayOfNeighbors = new VertexPosition[4];

        int arrayIndex = 0;
        foreach (var possibleNeighbor in VertexPosition.possibleNeighbors)
        {
            Vector3 position = new Vector3(currentVertex.X + possibleNeighbor.x, 0, currentVertex.Z + possibleNeighbor.y);

            // If currCell belongs to the grid
            if(grid.IsCellValid(position.x, position.z))
            {
                int index = grid.CalculateIndexFromCoordinates(position.x, position.z);
                arrayOfNeighbors[arrayIndex] = new VertexPosition(position, obstaclesArray[index]);
                arrayIndex++;
            }
        }
        return arrayOfNeighbors;
    }

    private static float ManhattanDistance(VertexPosition startVertex, VertexPosition exitVertex)
    {
        return Mathf.Abs(startVertex.X - exitVertex.X) + Mathf.Abs(startVertex.Z - exitVertex.Z);
    }
}
