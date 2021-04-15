using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUiController : MonoBehaviour
{
    [SerializeField]
    private Image healthFillImage;
    [SerializeField]
    private Text healthText;
    private EnemyController enemyController;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.gameObject;
        enemyController = enemy.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthFillImage == null)
            return;
        
        healthFillImage.fillAmount = enemyController.Health / enemyController.initialHealth;
        healthText.text = enemyController.Health + " / " + enemyController.initialHealth;
    }

    public void Initialization(GameObject healthBar)
    {
        var healthBarTrans = healthBar.transform;
        healthText = healthBarTrans.GetChild(1).GetComponent<Text>();
        healthFillImage = healthBarTrans.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void Destructor()
    {
        Destroy(healthText.transform.parent.gameObject);
        Destroy(this);
    }
}
