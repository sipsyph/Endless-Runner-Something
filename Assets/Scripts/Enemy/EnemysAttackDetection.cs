using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysAttackDetection : MonoBehaviour
{
    private bool playerInAttackRange, readyToAttack;
    private int ctr;
    void Start()
    {
        playerInAttackRange = false;
        readyToAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerInAttackRange)
        {
            ConstantForwardMovement();
            Debug.Log("Enemy is adjusting range to get closer");
        }else{
            RandomAttackEveryInterval(150);
        }
    }

    void RandomAttackEveryInterval(int interval)
    {
        ctr++;
        if(ctr>=interval)
        {
            ctr=0;
            Debug.Log("ATTACKSDETECTION => Current Enemy animator: "+EnemyAnimation.enemyAnimator.name);
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
            if(transform.parent.transform.name.Contains("Melee"))
            {

            }
            playerInAttackRange = true;
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player is NOT in attack range");
            if(transform.parent.transform.name.Contains("Melee"))
            {

            }
            playerInAttackRange = false;
        }
    }
}
