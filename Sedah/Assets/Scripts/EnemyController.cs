using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float  initialHealth, initialAttack, initialMovementSpeed, initialRotationSpeed, initialDetectRange, initialAttackRange;
    public float baseSpeedValue = 0.3f;
    public Transform attackPoint;
    public Transform player;
    public Transform Player { get => player; set => player = value; }

    private int level;
    private bool moveFlag = true;
    private Animator animator;
    public float health, attack, movementSpeed, rotationSpeed, detectRange, attackRange;
    public float Health { get => health; set => health = value; }
    public float Attack { get => attack; set => attack = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float DetectRange { get => detectRange; set => detectRange = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public EnemyAudioController enemyAudioController;

    private void Start() {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        level = PlayerPrefs.GetInt("Level");
        int maxLevel = PlayerPrefs.GetInt("MaxLevel");

        animator.speed = Mathf.Max((float)level / (float)maxLevel, baseSpeedValue);
        this.health = level * initialHealth;
        this.attack = level * initialAttack;

        float param = (1 + (float)(level - 1)/(float)maxLevel/2.0f);
        this.movementSpeed = param * initialMovementSpeed;
        this.rotationSpeed = param * initialRotationSpeed;
        this.detectRange = param * initialDetectRange;
        this.attackRange = param * initialAttackRange;

        enemyAudioController = GetComponent<EnemyAudioController>();
        enemyAudioController.SetPlayer(player);
    }

    private void Update() {
        if(player == null)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj == null)
                return;
            
            player = playerObj.transform;
        }

        if(health <= 0)
            return;
            
        if(moveFlag == false)
            return;

        // Look at player
        float distance = Vector3.Distance(player.position, transform.position);
        // Debug.Log(distance);
        if(distance < detectRange)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);

            if(distance < attackRange)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
                // EnemyAttack();
            }else{
                enemyAudioController.Run();
                
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }
        }else{
            enemyAudioController.Roar();

            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
        }
    }

    public void StartMovement()
    {
        this.moveFlag = true;
    }

    public void StopMovement()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
        this.moveFlag = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void EnemyAttack()
    {
        // Draws a sphere, or area in front of the player
        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange);
        foreach (Collider obj in hitObjects)
        {
            // Debug.Log(obj.name);
            if(obj.CompareTag("Player"))
            {
                if(obj == null || obj.GetComponent<PlayerController>() == null)
                    return;
                
                enemyAudioController.Attack();
                obj.GetComponent<PlayerController>().Damage(attack);
            }
        }
    }

    public void Damage(float dmg)
    {
        enemyAudioController.Damage();
        Debug.Log("Enemy got damaged by " + dmg);
        health -= dmg;
        if(health <= 0)
        {
            health = 0;
            animator.speed = 1;
            animator.SetTrigger("Dead");
        }else{
            animator.SetTrigger("Damage");
        }
    }

    private void Dead()
    {
        // TODO: animation calls destroy(gameobject)
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>().EnemyKilled();
        GetComponent<EnemyUiController>().Destructor();
        GetComponent<EnemyAudioController>().Destructor();

        Destroy(gameObject);
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    // }

}
