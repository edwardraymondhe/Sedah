using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUiController : MonoBehaviour
{
    [SerializeField]
    private GameObject openButton;
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private TextMeshProUGUI health;
    [SerializeField]
    private TextMeshProUGUI lifeCount;
    [SerializeField]
    private TextMeshProUGUI attack;
    [SerializeField]
    private TextMeshProUGUI range;
    [SerializeField]
    private TextMeshProUGUI speed;

    private PlayerController playerController;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
            playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        if(playerController == null)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            panel.SetActive(false);
            openButton.SetActive(true);
            closeButton.SetActive(false);
        }

        health.SetText(playerController.Health.ToString());
        lifeCount.SetText(playerController.LifeCount.ToString());
        attack.SetText(playerController.Attack.ToString());
        range.SetText(playerController.Range.ToString());
        speed.SetText(playerController.Speed.ToString());

    }

    public void ControlPanel()
    {
        openButton.SetActive(panel.activeInHierarchy);
        closeButton.SetActive(!panel.activeInHierarchy);
        // openButton.SetActive(!openButton.activeInHierarchy);
        panel.SetActive(!panel.activeInHierarchy);
    }
}
