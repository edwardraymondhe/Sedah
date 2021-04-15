using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    public float initialMovementSpeed, initialRotationSpeed, initialDetectDistance, initialAttackRange;
    private Transform player;
    private float movementSpeed, rotationSpeed, detectDistance, attackRange;

    public Transform Player { get => player; set => player = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float DetectDistance { get => detectDistance; set => detectDistance = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }

    private void Start() {
        this.movementSpeed = initialMovementSpeed;
        this.rotationSpeed = initialRotationSpeed;
        this.detectDistance = initialDetectDistance;
        this.attackRange = initialAttackRange;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        // Look at player
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance < detectDistance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);

            if(distance < attackRange)
            {
                Attack();
            }
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        
    }
}
