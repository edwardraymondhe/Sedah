using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    // Position for the cell
    private int x, z;
    private bool isTaken;
    private CellObjectType objectType;
    public int X { get => x;}
    public int Z { get => z;}
    public bool IsTaken { get => isTaken; set => isTaken = value; }
    public CellObjectType ObjectType { get => objectType; set => objectType = value; }

    public Cell(int x, int z)
    {
        this.x = x;
        this.z = z;
        this.objectType = CellObjectType.Empty;
        this.isTaken = false;
    }
}

// What's inside our cell
public enum CellObjectType
{
    Empty,
    Road,
    Obstacle,
    Start,
    Exit
}
