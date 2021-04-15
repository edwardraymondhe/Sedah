using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int baseMapComplexity = 5;
    public int maxLevel = 5;
    public int level = 1;

    public int initialGridWidth = 11;
    public int initialGridLength = 11;
    public int mapComplexity = 0;
    private int gridWidth, gridLength, enemyCount;
    public int actualEnemyCount;
    private MapGenerator mapGenerator;
    private Light globalLight;
    public int ActualEnemyCount { get => actualEnemyCount; set => actualEnemyCount = value; }

    public GameObject pauseCanvas;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(SceneManager.GetActiveScene().name != "GameScene")
            return;

        globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light>();

        maxLevel = PlayerPrefs.GetInt("MaxLevel");
        mapComplexity = PlayerPrefs.GetInt("MapComplexity");

        CalculateInitParam();

        mapGenerator = GetComponentInChildren<MapGenerator>();
        if(mapGenerator != null)
        {
            mapGenerator.SetParam(mapComplexity, gridWidth, gridLength, enemyCount);
            mapGenerator.Initialization();
        }
    }

    private void Update() {
        if(pauseCanvas == null)
            return;

        if(Input.GetKeyDown(KeyCode.Escape))
            ControlPause();
    }

    void CalculateInitParam()
    {
        PlayerPrefs.SetInt("Level", level);

        mapComplexity += baseMapComplexity;

        gridWidth = initialGridWidth + 3 * level;
        gridLength = initialGridLength + 3 * level;

        enemyCount = 3 * mapComplexity * level;
        // enemyCount = 0;

        globalLight.intensity = (float)(maxLevel - level + 1) / (float)maxLevel;
    }

    public void FinishLevel()
    {
        if(level == maxLevel)
        {
            WonGame();
            // TODO: End Scene
            return;
        }

        if(actualEnemyCount <= 0)
        {
            // Send to transition scene, preserve current gameobject
            // Send player to specific spot
            mapGenerator.gridVisualizer.Initialization();
            mapGenerator.mapVisualizer.ClearMap();

            StartCoroutine(FadeOut("TransitionScene"));
        }
    }
    IEnumerator FadeOut(string sceneName)
    {
        GameObject.FindGameObjectWithTag("SceneTransitionImage").GetComponent<TransitionImageController>().FadeOut();

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
    public void LoadLevel()
    {
        level++;
        CalculateInitParam();
        Debug.Log(PlayerPrefs.GetInt("Level"));
        mapGenerator.SetParam(level, gridWidth, gridLength, enemyCount);
        mapGenerator.Initialization();
    }

    public void WonGame()
    {
        // Change scene to won
        PlayerPrefs.SetString("Result", "YOU WON");
        PlayerPrefs.SetFloat("Score", player.GetComponent<PlayerController>().totalScore);
        GetComponentInChildren<BackgroundAudioController>().StopMusic();
        StartCoroutine(FadeOut("EndScene"));
    }

    public void LostGame()
    {
        PlayerPrefs.SetString("Result", "YOU LOST");
        PlayerPrefs.SetFloat("Score", player.GetComponent<PlayerController>().totalScore);
        GetComponentInChildren<BackgroundAudioController>().StopMusic();
        StartCoroutine(FadeOut("EndScene"));
    }

    public void EnemyKilled()
    {
        actualEnemyCount -= 1;
    }
    public void StopEnemyAnimation()
    {
        mapGenerator.mapVisualizer.StopEnemyAnimation();
    }
    public void StartEnemyAnimation()
    {
        mapGenerator.mapVisualizer.StartEnemyAnimation();
    }

    public void ControlPause()
    {
        Time.timeScale = 1 - Time.timeScale;
        pauseCanvas.SetActive((int)Time.timeScale == 0);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        // mapGenerator.mapVisualizer.ClearMap();
        GetComponentInChildren<BackgroundAudioController>().StopMusic();
        StartCoroutine(FadeOut("StartScene"));
    }
}
