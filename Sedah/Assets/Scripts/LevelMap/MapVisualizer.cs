using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapVisualizer : MonoBehaviour
{
    private Transform parent;
    public Color startColor, exitColor;
    // public GameObject roadTileStraight, roadTileCorner, emptyTile, startTile, exitTile;
    // public GameObject[] environmentTiles;
    public List<EnvironmentSO> environmentArray = new List<EnvironmentSO>();
    public GameObject playerPrefab;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> bossPrefabs;
    Dictionary<Vector3, GameObject> dictionaryOfObstacles = new Dictionary<Vector3, GameObject>();

    public GameObject enemies, items;
    public GameObject enemyHealthBar, followCanvas;
    private PlayerController playerController;
    private List<Animator> enemyAnimators = new List<Animator>();

    public void StopEnemyAnimation()
    {
        foreach (Animator animator in enemyAnimators)
        {
            animator.speed = 0;
            animator.GetComponent<EnemyController>().StopMovement();
        }
    }

    public void StartEnemyAnimation()
    {
        if(enemyAnimators.Count <= 0)
            return;

        EnemyController enemy = enemyAnimators[0].gameObject.GetComponentInParent<EnemyController>();
        int level = PlayerPrefs.GetInt("Level");
        int maxLevel = PlayerPrefs.GetInt("MaxLevel");
        float speed = Mathf.Max((float)level / (float)maxLevel, enemy.baseSpeedValue);
        
        foreach (Animator animator in enemyAnimators)
        {
            animator.speed = speed;
            animator.GetComponent<EnemyController>().StartMovement();
        }
    }

    private void Awake() {
        parent = this.transform;
    }

    public void VisualizeMap(MapGrid grid, MapData data, bool visualizeUsingPrefabs)
    {
        if(visualizeUsingPrefabs)
        {
            VisualizeUsingPrefabs(grid, data);
        }
        else
        {
            VisualizeUsingPrimitives(grid, data);
        }
    }

    private void VisualizeUsingPrefabs(MapGrid grid, MapData data)
    {
        for (int i = 0; i < data.path.Count; i++)
        {
            var position = data.path[i];
            if(position != data.exitPosition)
            {
                grid.SetCell(position.x, position.z, CellObjectType.Road);
            }
        }
        

        int environmentIndex = Random.Range(0, environmentArray.Count);
        EnvironmentSO environment = environmentArray[environmentIndex];

        int enemyCount = data.enemyCount;
        int actualEnemyCount = 0;
        for (int col = 0; col < grid.Width; col++)
        {
            for (int row = 0; row < grid.Length; row++)
            {
                var cell = grid.GetCell(col, row);
                var position = new Vector3(cell.X, 0, cell.Z);

                var index = grid.CalculateIndexFromCoordinates(position.x, position.z);
                if(data.obstacleArray[index] && cell.IsTaken == false)
                {
                    cell.ObjectType = CellObjectType.Obstacle;
                }
    
                switch (cell.ObjectType)
                {
                    case CellObjectType.Empty:
                        int decDice = Random.Range(0, 2);
                        if(decDice == 1)
                            CreateCharacter(position, environment.decorations); // Decorations

                        CreateIndicator(position, environment.empty);   // Base
                        break;
                    case CellObjectType.Road:
                        CreateIndicator(position, environment.empty);    // Base
                        break;
                    case CellObjectType.Obstacle:
                        bool createCharacter = false;
                        if(enemyCount > 0)
                        {
                            int dice = Random.Range(0, 2);
                            if(dice == 1)
                                createCharacter = true;
                        }
                        if(createCharacter || actualEnemyCount == 0)
                        {
                            GameObject enemy;
                            if(actualEnemyCount == 0 && (PlayerPrefs.GetInt("Level") == PlayerPrefs.GetInt("MaxLevel")))
                            {
                                // Enemy
                                int bossIndex = Random.Range(0, bossPrefabs.Count);
                                enemy = CreateCharacter(position, bossPrefabs[bossIndex]);
                            }else{
                                // Enemy
                                int enemyIndex = Random.Range(0, enemyPrefabs.Count);
                                enemy = CreateCharacter(position, enemyPrefabs[enemyIndex]);
                            }
                            enemy.transform.SetParent(enemies.transform);

                            // Base
                            CreateIndicator(position, environment.empty);

                            // UI following enemy
                            var healthBar = Instantiate(enemyHealthBar, Vector3.zero, new Quaternion());
                            healthBar.transform.SetParent(followCanvas.transform);
                            healthBar.GetComponent<UiFollowWorld>().lookAt = enemy.transform;
                            enemy.GetComponent<EnemyUiController>().Initialization(healthBar);

                            enemyAnimators.Add(enemy.GetComponent<Animator>());

                            // Enemy count
                            actualEnemyCount++;
                            enemyCount--;
                        }else{
                            CreateIndicator(position, environment.empty);       // Base
                            CreateCharacter(position, environment.obstacles);   // Obstacle
                        }
                        break;
                    case CellObjectType.Start:
                        CreateCharacter(new Vector3(position.x, position.y + 0.5f, position.z) , environment.startSymbol);   // Symbol
                        CreateIndicator(position, environment.start);   // Base
                        
                        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(position.x, position.y + 1f, position.z);
                        break;
                    case CellObjectType.Exit:
                        CreateCharacter(new Vector3(position.x, position.y + 0.5f, position.z) , environment.exitSymbol);   // Symbol

                        var exit = CreateIndicator(position, environment.exit); // Player
                        exit.AddComponent<ExitPointController>();
                        break;
                    default:
                        break;
                }
            }
        }

        GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>().ActualEnemyCount = actualEnemyCount;
    }

    private void CreateIndicator(Vector3 position, List<GameObject> prefabs, Quaternion rotation = new Quaternion())
    {
        int randomIndex = Random.Range(0, prefabs.Count);
        CreateIndicator(position, prefabs[randomIndex]);
    }

    private GameObject CreateCharacter(Vector3 position, GameObject prefab, Quaternion rotation = new Quaternion())
    {
        Vector3 characterPosition = new Vector3(position.x, position.y + 0.5f, position.z);
        return CreateIndicator(characterPosition, prefab);
    }

    private GameObject CreateCharacter(Vector3 position, List<GameObject> prefabs, Quaternion rotation = new Quaternion())
    {
        int randomIndex = Random.Range(0, prefabs.Count);
        Vector3 characterPosition = new Vector3(position.x, position.y + 0.5f, position.z);
        return CreateIndicator(characterPosition, prefabs[randomIndex]);
    }

    private GameObject CreateIndicator(Vector3 position, GameObject prefab, Quaternion rotation = new Quaternion())
    {
        var placementPosition = position + new Vector3(0.5f, 0.5f, 0.5f);
        var element = Instantiate(prefab, placementPosition, rotation);
        element.transform.parent = parent;
        dictionaryOfObstacles.Add(position, element);
        return element;
    }

    private void VisualizeUsingPrimitives(MapGrid grid, MapData data)
    {
        PlaceStartAndExitPoints(data);

        for(int i = 0; i < data.obstacleArray.Length; i++)
        {
            if(data.obstacleArray[i])
            {
                var positionOnGrid = grid.CalculateCoordinatesFromIndex(i);
                if(positionOnGrid == data.startPosition || positionOnGrid == data.exitPosition)
                {
                    continue;
                }
                grid.SetCell(positionOnGrid.x, positionOnGrid.z, CellObjectType.Obstacle);

                // Placed an knight
                if(PlaceKnightObstacle(data, positionOnGrid))
                {
                    continue;
                }

                if(dictionaryOfObstacles.ContainsKey(positionOnGrid) == false)
                {
                    CreateIndicator(positionOnGrid, Color.white, PrimitiveType.Cube);
                }
            }
        }
    }

    private bool PlaceKnightObstacle(MapData data, Vector3 positionOnGrid)
    {
        foreach (var knight in data.knightPiecesList)
        {
            if(knight.Position == positionOnGrid)
            {
                CreateIndicator(positionOnGrid, Color.red, PrimitiveType.Cube);
                return true;
            }
        }

        return false;
    }

    private void PlaceStartAndExitPoints(MapData data)
    {
        CreateIndicator(data.startPosition, startColor, PrimitiveType.Sphere);
        CreateIndicator(data.exitPosition, exitColor, PrimitiveType.Sphere);
    }

    private void CreateIndicator(Vector3 position, Color color, PrimitiveType sphere)
    {
        var element = GameObject.CreatePrimitive(sphere);
        dictionaryOfObstacles.Add(position, element);
        element.transform.position = position + new Vector3(0.5f, 0.5f, 0.5f);
        element.transform.parent = parent;
        var renderer = element.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", color);
    }

    public void ClearMap()
    {
        for(int i = followCanvas.transform.childCount; i > 0; i--)
        {
            Destroy(followCanvas.transform.GetChild(i - 1).gameObject);
        }
        foreach (var obstacle in dictionaryOfObstacles.Values)
        {
            Destroy(obstacle);
        }
        foreach (var animator in enemyAnimators)
        {
            Destroy(animator);
        }
        enemyAnimators.Clear();

        dictionaryOfObstacles.Clear();
    }
}
