using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysDetection : MonoBehaviour
{
    public GameObject bow, meleeRangeDetector;
    private bool playerDetected, enemyIsMelee, playerExited;
    public float movementSpeed, rotationSpeed;

    private int ctrBeforeDeath;

    private Transform enemyTransform;
    private string enemyName;

    private Vector3 startingPosition;

    private Quaternion startingRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        playerExited = false;
        enemyTransform = transform.parent.transform;
        enemyName = enemyTransform.name;
        if(enemyName.Contains("Melee"))
        {
            enemyIsMelee = true;
        }else{
            enemyIsMelee = false;
        }
        movementSpeed = 2f;
        rotationSpeed = 3f;
        startingPosition = enemyTransform.localPosition;
        startingRotation = enemyTransform.localRotation;
        //Debug.Log("Starting Position of "+enemyName+": "+startingPosition);
    }

    void Update()
    {  
        HandleEnemyDeath(); //Actively observe if Enemy has died to player
        if(playerDetected && (!isEnemyDead()))
        {
            RotateTowardsPlayer();
            if(enemyIsMelee)
            {
                meleeRangeDetector.SetActive(true);
            }
        }
       
    }

    bool isEnemyDead()
    {
        return (PlayerParent.currentEnemy == enemyTransform && PlayerParent.currentEnemyIsDead);
    }

    void RotateTowardsPlayer()
    {
        var newRotation = Quaternion.LookRotation(PlayerParent.playerHeadStatic.position - transform.parent.transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            enemyTransform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

    }

    void ConstantForwardMovement()
    {
        if(!PlayerParent.enemyDetected)
        {
            enemyTransform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }

    void HandleEnemyDeath()
    {
        
        if(isEnemyDead())
        {
            Debug.Log("Entered Death code for"+enemyName);
            ctrBeforeDeath = 0;
            if(enemyIsMelee)
            {
                Debug.Log("Entered Enemy isMelee code DEAATH");
                meleeRangeDetector.SetActive(false);
            }else{
                Debug.Log("Entered Enemy isRanged code DEAATH");
                bow.SetActive(false);
                PlayerParent.projectileIncomingIndicatorStatic.SetActive(false);
            }
            EnemyAnimation.ResetAllInCurrentTriggerSetExcept("IdleTrigger");
            enemyTransform.localRotation = startingRotation;
            enemyTransform.localPosition = startingPosition;
            playerDetected = false;
            PlayerParent.currentEnemyIsDead = false;
            playerExited = false;
            
            enemyTransform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {   
            PlayerParent.currentEnemy = enemyTransform;
            PlayerParent.currentEnemyHealth = enemyTransform.gameObject.GetComponent<Adversary>().health;
            
            //Debug.Log("Current Enemy animator: "+EnemyAnimation.enemyAnimator.name);
            playerDetected = true;
            //Debug.Log(transform.parent.transform.name+" has detected player");
            EnemyAnimation.enemyAnimator = enemyTransform.gameObject.GetComponent<Animator>();
            
            
            if(enemyIsMelee)
            {
                EnemyAnimation.currentSetOfTriggers = EnemyAnimation.koboldSwordTriggerNames;
                
            }else{
                EnemyAnimation.currentSetOfTriggers = EnemyAnimation.koboldBowTriggerNames;
                
                PlayerParent.enemyDetected = true;
                bow.SetActive(true);
                //EnemyAnimation.PlayKoboldBowShootAnimation();
            }
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            //When this occurs, it should mean that the player ran away from the enemy
            //without getting locked on to it
            Debug.Log("Player has dodged fight with "+enemyName);
            playerExited = true;
        }
    }
    
}
