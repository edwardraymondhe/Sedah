using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public GameObject groundPrefab;
    private GameObject board;
    private List<GameObject> borders = new List<GameObject>();
    public GameObject VisualizeWall(Vector3 position, Vector3 scale)
    {
        var border = GameObject.CreatePrimitive(PrimitiveType.Cube);
        border.transform.position = position;
        border.transform.localScale = scale;
        border.transform.SetParent(transform.parent);
        border.GetComponent<Renderer>().enabled = false;
        return border;
    }
    public void VisualizeGrid(int width, int length)
    {
        Initialization();
        
        borders.Add(VisualizeWall(new Vector3(-0.5f, 0.5f, length / 2f), new Vector3(1, 100, length + 2)));
        borders.Add(VisualizeWall(new Vector3(width + 0.5f, 0.5f, length / 2f), new Vector3(1, 100, length + 2)));
        borders.Add(VisualizeWall(new Vector3(width / 2f, 0.5f, -0.5f), new Vector3(width, 100, 1)));
        borders.Add(VisualizeWall(new Vector3(width / 2f, 0.5f, length + 0.5f), new Vector3(width, 100, 1)));

        Vector3 position = new Vector3(width / 2f, 0, length / 2f);
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        board = Instantiate(groundPrefab, position, rotation);
        board.transform.localScale = new Vector3(width, length, 1);
        board.transform.SetParent(transform.parent);
    }

    public void Initialization()
    {
        if(board != null)
        {
            Destroy(board);
        }

        if(borders.Count > 0)
        {
            foreach (var border in borders)
            {
                Destroy(border);
            }
        }
    }
}
