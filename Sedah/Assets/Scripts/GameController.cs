using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TutorialCanvasController tutorialCanvas;
    public PlayerStatusUiController playerStatusUiCanvas;
    public GameObject levelControllerPrefab;
    GameObject levelControllerInstance;
    // Start is called before the first frame update
    void Start()
    {
        levelControllerInstance = GameObject.FindGameObjectWithTag("LevelController");
        if(levelControllerInstance == null)
        {
            levelControllerInstance = Instantiate(levelControllerPrefab);
        }else{
            LevelController levelController = levelControllerInstance.GetComponent<LevelController>();
            Debug.Log("Before loadLevel");
            levelController.LoadLevel();
        }

        Debug.Log("After loadLevel");
        if(PlayerPrefs.GetInt("Level") == 1)
        {
            Debug.Log("In if");
            tutorialCanvas.ControlPanel();
            playerStatusUiCanvas.ControlPanel();
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
