using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject settingPanel;
    public float mapComplexity = 1;
    public float maxLevel = 1;
    private void Start() {
        var levelController = GameObject.FindGameObjectWithTag("LevelController");
        if(levelController != null)
            Destroy(levelController);
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("MapComplexity", (int)mapComplexity);
        PlayerPrefs.SetInt("MaxLevel", (int)maxLevel);
        PlayerPrefs.SetInt("Level", 1);

        SceneManager.LoadScene("GameScene");
    }
    public void OpenSettingsPanel()
    {
        startPanel.SetActive(false);
        settingPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        startPanel.SetActive(true);
        settingPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetMaxLevel(float level)
    {
        maxLevel = level;
    }
    public void SetMapComplexity(float complexity)
    {
        mapComplexity = complexity;
    }

}
