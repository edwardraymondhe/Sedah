using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialHealth;
    public float initialAttack;
    public float initialSpeed;
    public float initialRange;
    public float initialLifeCount;
    public float health, attack, speed, range, lifeCount;
    private Animator animator;
    private Rigidbody rb;
    public Transform attackPoint;

    public float Health { get => health; set => health = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Range { get => range; set => range = value; }
    public float LifeCount { get => lifeCount; set => lifeCount = value; }


    private PlayerAudioController playerAudioController;
    public bool reviveFlag = false;
    private bool stopFlag = false;
    private float prevSpeed;
    private float currSprintTime = 0f;
    private float sprintLimitTIme = 0.25f;
    private bool sprintFlag = false;
    public float currCoolTime = 0.0f;
    public float sprintCoolLimitTime = 5.0f;

    public float totalScore = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.health = initialHealth;
        this.attack = initialAttack;
        this.speed = initialSpeed;
        this.range = initialRange;
        this.lifeCount = initialLifeCount;
        Debug.Log("Start: " + speed);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        playerAudioController = GetComponent<PlayerAudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeCount > 0)
        {
            if(!stopFlag)
            {
                Move();
                Action();
            }
        }
    }

    private void FixedUpdate() {
        if(sprintFlag)
            currSprintTime += Time.deltaTime;
        else
            currCoolTime += Time.deltaTime;
        
        if(sprintFlag && currSprintTime >= sprintLimitTIme)
        {
            currSprintTime = 0f;
            sprintFlag = false;
            speed = prevSpeed;
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

    public void Damage(float dmg)
    {
        if(reviveFlag == true || lifeCount <= 0)
            return;
            
        totalScore -= dmg;
        health -= dmg;
        if(health <= 0)
        {
            health = 0;
            lifeCount--;
            animator.SetBool("Revive", lifeCount > 0);
            animator.SetTrigger("Dead");
        }else{
            animator.SetTrigger("Damage");
        }
    }
    public void StopAllAnimation()
    {
        stopFlag = false;
        StopEnemyAnimation();
    }

    public void StartAllAnimation()
    {
        stopFlag = true;
        StartEnemyAnimation();
    }

    public void StopEnemyAnimation()
    {
        LevelController levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        levelController.StopEnemyAnimation();
    }

    public void StartEnemyAnimation()
    {
        LevelController levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        levelController.StartEnemyAnimation();
    }

    private void Action()
    {
        animator.ResetTrigger("SingleAttack");
        // animator.ResetTrigger("RangeAttack");

        if(Input.GetMouseButtonDown(0))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // SprintAttack();
            }else{
                // LookAtTarget();
                animator.SetTrigger("SingleAttack");
            }
        }else if(Input.GetMouseButtonDown(1))
        {
            // LookAtTarget();
            animator.SetTrigger("RangeAttack");
        }
        // Debug.Log("Sprint: " + speed);

    }


    private void SprintAttack()
    {
    }

    private void Sprint()
    {
        // Instantiate particle effect
        // Boosts the character
        if(currCoolTime < sprintCoolLimitTime)
            return;

        prevSpeed = speed;
        speed *= 3;
        sprintFlag = true;

        currCoolTime = 0f;

        animator.SetTrigger("Sprint");
    }

    private void EndSprint()
    {
        speed = prevSpeed;
    }

    private void MeleeAttack()
    {
        // Triggers the animation
        // TODO: Slows down the speed

        // Draws a sphere, or area in front of the player
        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, range);
        foreach (Collider obj in hitObjects)
        {
            // Check tag
            string objTag = obj.tag;
            switch (objTag)
            {
                case "Enemy":
                    playerAudioController.Attack();
                    obj.GetComponent<EnemyController>().Damage(attack);
                    totalScore += attack;
                    break;
                
                default:
                    // obj.GetComponent<ItemController>().Damage();
                    break;
            }
        }
        // Any objects collides with the sphere calls Attacked()
        //  Their reaction depends on their own
    }

    private void RangeAttack()
    {
        throw new NotImplementedException();
    }

    private void Dead()
    {
        // Debug.Log("Sprint: " + speed);

        if(lifeCount <= 0)
        {
            // Destroy(gameObject);
            GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>().LostGame();
            Destroy(this);
        }
    }

    void Move()
    {
        // Debug.Log("Sprint: " + speed);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement.Normalize();
        if(movement.magnitude > 0)
        {
            Debug.Log("Running");
            playerAudioController.Run();
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
            gameObject.transform.forward = movement;
        }

        animator.SetBool("Run", movement.magnitude > 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!sprintFlag)
                Sprint();
        }else{
            animator.ResetTrigger("Sprint");
        }
    }

    public void Revive()
    {
        health = initialHealth;
    }

    public void Reviving()
    {
        reviveFlag = !reviveFlag;
    }
}
