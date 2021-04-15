using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialCanvasController : MonoBehaviour
{    
    [SerializeField]
    private GameObject openButton;
    [SerializeField]
    private GameObject closeButton;
    public GameObject panel;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            panel.SetActive(false);
            openButton.SetActive(true);
            closeButton.SetActive(false);
        }
    }

    public void ControlPanel()
    {
        openButton.SetActive(panel.activeInHierarchy);
        closeButton.SetActive(!panel.activeInHierarchy);

        panel.SetActive(!panel.activeInHierarchy);

        Time.timeScale = 1;
    }
}
