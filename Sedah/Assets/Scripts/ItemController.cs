using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;
    public ParticleSystem particle;
    public TransitionRoomController transitionRoomController;
    private void Start() {
        Initialization();
    }
    void Update()
    {
        if(item == null)
            return;
    }

    public void Initialization()
    {
        if(item.itemType == ItemType.special)
        {
            int level = PlayerPrefs.GetInt("Level");
            int maxLevel = PlayerPrefs.GetInt("MaxLevel");

            // Sets between 0.2 and 0.8
            float setSize = 0.2f + (0.8f - 0.2f) * ((float)(level) / (float) maxLevel);
            SetParticleSize(setSize);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            TriggerItemEffect(player);

            transitionRoomController.DeleteOtherPowerUps(GetComponent<ItemController>());
        }
    }

    private void TriggerItemEffect(PlayerController playerController)
    {
        int level = PlayerPrefs.GetInt("Level");
        playerController.LifeCount += item.lifeCount * level;
        playerController.Attack += item.attack * level;
        playerController.Health += item.health * level;
        playerController.Range += item.attackRange * level;
        playerController.Speed += item.movementSpeed * level;

        playerController.Attack += 0.05f * playerController.Attack;
        playerController.Health += 0.05f * playerController.Health;
        playerController.Range += 0.05f * playerController.Range;
        playerController.Speed += 0.05f * playerController.Speed;
    }

    private void SetParticleSize(float size)
    {
        particle = GetComponentInChildren<ParticleSystem>();
        var main = particle.main;
        main.startSize = new ParticleSystem.MinMaxCurve(size, size);
    }
}
