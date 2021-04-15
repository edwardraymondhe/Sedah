using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionRoomController : MonoBehaviour
{
    public List<GameObject> powerUps;
    public List<Transform> powerUpPositions;
    private List<ItemController> createPowerUps = new List<ItemController>();
    public GameObject triggerPowerUpEffect;
    public Transform playerSpawnStatus;
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnStatus.position;
        player.transform.rotation = playerSpawnStatus.rotation;

        CreatePowerUps();
    }

    private void CreatePowerUps()
    {
        List<GameObject> currCreatePowerUps = new List<GameObject>();

        while(currCreatePowerUps.Count < 3)
        {
            GameObject selectedPowerUp = powerUps[Random.Range(0, powerUps.Count)];
            if(currCreatePowerUps.Find(obj => obj == selectedPowerUp) == null)
                currCreatePowerUps.Add(selectedPowerUp);
        }

        for (int i = 0; i < powerUpPositions.Count; i++)
        {
            var obj = Instantiate(currCreatePowerUps[i], powerUpPositions[i].position, new Quaternion());
            ItemController itemController = obj.GetComponent<ItemController>();
            itemController.transitionRoomController = GetComponent<TransitionRoomController>();
            createPowerUps.Add(itemController);
        }
    }

    public void DeleteOtherPowerUps(ItemController itemController)
    {
        foreach (var item in createPowerUps)
        {
            if(item == itemController)
            {
                Instantiate(triggerPowerUpEffect, item.transform.position, new Quaternion());
            }

            Destroy(item.gameObject);
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        GameObject.FindGameObjectWithTag("SceneTransitionImage").GetComponent<TransitionImageController>().FadeOut();
        
        yield return new WaitForSeconds(1);

        GoToNextLevel();
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
}
