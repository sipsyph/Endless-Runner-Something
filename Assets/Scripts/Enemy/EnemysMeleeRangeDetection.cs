using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysMeleeRangeDetection : MonoBehaviour
{
    private bool playerInAttackRange, readyToAttack;
    public int baseFrameIntervalBetweenAttacks;
    private int ctr;

    private Transform enemyTransform;
    private string enemyTransformName;
    void Start()
    {
        enemyTransform = transform.parent.transform;
        enemyTransformName = enemyTransform.name;
        playerInAttackRange = false;
        readyToAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyDead())
        {
            playerInAttackRange = false;
        }

        if(enemyTransformName.Contains("Melee") && !isEnemyDead())
        {
            Debug.Log("ENTERING PLAYER IN ATTACK RANGE CODE");
            if(!playerInAttackRange)
            {
                ConstantForwardMovement();
                Debug.Log("Enemy is adjusting range to get closer");
            }
            else{
                RandomAttackEveryInterval(baseFrameIntervalBetweenAttacks);
                //HandleWhenPlayerBlocksAttack();
            }
        }

    }

    void RandomAttackEveryInterval(int interval)
    {
        ctr++;
        if(ctr>=interval)
        {
            ctr=0;
            DetermineTargetSlot();
        }
    }

    void HandleWhenPlayerBlocksAttack()
    {
        if(PlayerShield.shieldBlocked)
        {
            Debug.Log("Playing enemy got blocked animation");
            EnemyAnimation.PlayBlockedAnimation();
        }
    }

    private void DetermineTargetSlot()
    {
        Debug.Log("Enemy is Attacking");
        //TODO: Handle different enemies' animations here
        int randNum = Random.Range(1,100);

        if(enemyTransformName.Contains("Slime"))
        {
            Debug.Log("Enemy is SLIME");
            EnemyAnimation.PlayLeftAttackAnimation();
            return;
        }
        if(enemyTransformName.Contains("Kobold Sword"))
        {
            if(randNum>=1 && randNum<=25)
            {
                EnemyAnimation.PlayUpperLeftSwingAnimation();
            }
            else if(randNum>25 && randNum<=50)
            {
                EnemyAnimation.PlayUpperRightSwingAnimation();
            }
            else if(randNum>50 && randNum<=75)
            {
                EnemyAnimation.PlayBottomLeftSwingAnimation();
            }
            else
            {
                EnemyAnimation.PlayBottomRightSwingAnimation();
            }
        }
        
    }

    void ConstantForwardMovement()
    {
        enemyTransform.Translate(Vector3.forward * Time.deltaTime * 1);
        EnemyAnimation.PlayWalkingAnimation();
    }


    bool isEnemyDead()
    {
        return (PlayerParent.currentEnemy == enemyTransform && PlayerParent.currentEnemyIsDead);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player is in attack range");
            PlayerParent.enemyDetected = true;
            playerInAttackRange = true;
            PlayerParent.isInAttackRange = true;
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player is NOT in attack range");
            if(enemyTransformName.Contains("Melee"))
            {
                playerInAttackRange = false;
            }else{
                PlayerParent.isInAttackRange = false;
                playerInAttackRange = false;
            }
            
        }
    }
}
