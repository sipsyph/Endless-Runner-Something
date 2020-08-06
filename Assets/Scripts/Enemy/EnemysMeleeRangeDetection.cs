using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysMeleeRangeDetection : MonoBehaviour
{
    private bool playerInAttackRange, readyToAttack;
    public int baseFrameIntervalBetweenAttacks;
    private int ctr;
    void Start()
    {
        playerInAttackRange = false;
        readyToAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent.transform.name.Contains("Melee"))
        {
            if(!playerInAttackRange)
            {
                //ConstantForwardMovement();
                Debug.Log("Enemy is adjusting range to get closer");
            }
            else{
                RandomAttackEveryInterval(baseFrameIntervalBetweenAttacks);
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

    private void DetermineTargetSlot()
    {
        int randNum = Random.Range(1,100);
        if(randNum>=1 && randNum<=25)
        {
            EnemyAnimation.PlayUpperLeftSwingAnimation();
            //EnemyAnimation.PlayBottomLeftSwingAnimation();
        }
        else if(randNum>25 && randNum<=50)
        {
            EnemyAnimation.PlayUpperRightSwingAnimation();
            //EnemyAnimation.PlayBottomLeftSwingAnimation();
        }
        else if(randNum>50 && randNum<=75)
        {
            //EnemyAnimation.PlayUpperLeftSwingAnimation();
            EnemyAnimation.PlayBottomLeftSwingAnimation();
        }
        else
        {
            //EnemyAnimation.PlayUpperRightSwingAnimation();
            EnemyAnimation.PlayBottomRightSwingAnimation();
        }

        
    }

    void ConstantForwardMovement()
    {
        transform.parent.transform.Translate(Vector3.forward * Time.deltaTime * 1);
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
            if(transform.parent.transform.name.Contains("Melee"))
            {
                
            }else{
                PlayerParent.isInAttackRange = false;
                playerInAttackRange = false;
            }
            
        }
    }
}
