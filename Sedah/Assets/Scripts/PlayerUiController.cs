using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUiController : MonoBehaviour
{
    [SerializeField]
    private Image healthFillImage;
    [SerializeField]
    private TextMeshProUGUI healthText;
    
    [SerializeField]
    private Image lowHealthFilterImage;

    [SerializeField]
    private TextMeshProUGUI lifeCountText;
    
    [SerializeField]
    private TextMeshProUGUI sprintText;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    private PlayerController playerController;
    private GameObject player;

    // Start is called before the first frame update
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

        float ratio = Mathf.Clamp(playerController.Health / playerController.initialHealth, 0, 1);
        Color color = lowHealthFilterImage.color;
        lowHealthFilterImage.color = new Color(color.r, color.g, color.b, 1 - ratio);
        healthFillImage.fillAmount = ratio;
        healthText.SetText(playerController.Health + "/" + playerController.initialHealth);
        
        lifeCountText.SetText(playerController.LifeCount + "/" + playerController.initialLifeCount); 

        sprintText.SetText(((int)Mathf.Clamp(playerController.currCoolTime / playerController.sprintCoolLimitTime, 0.0f, 1.0f)).ToString() + "/1");

        scoreText.SetText(playerController.totalScore.ToString());
    }

}
