using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI score;
    private float currTime = 0f;
    private float countTime = 3.5f;    
    private void Start() {
        textMesh.SetText("\"" + PlayerPrefs.GetString("Result") + "\"");
    }
    private void Update() {
        if(currTime < countTime)
        {
            currTime += Time.deltaTime;

            score.SetText(Random.Range(0, 1000).ToString());
        }else{
            score.SetText(PlayerPrefs.GetFloat("Score").ToString());
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
