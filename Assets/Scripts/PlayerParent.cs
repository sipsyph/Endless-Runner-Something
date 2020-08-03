using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{

    public static Transform currentEnemy, playerBodyStatic, playerHeadStatic;
    public static bool enemyDetected, isAttacking;

    public static int currentEnemyHealth, attackingModeDurationCtr;

    private bool hittingLeftAreaBlocker, hittingRightAreaBlocker;

    public Transform mainCamera, playerBody, playerHead;
    void Start()
    {
        playerBodyStatic = playerBody;
        playerHeadStatic = playerHead;
        enemyDetected = false;
        hittingLeftAreaBlocker = false;
        hittingRightAreaBlocker = false;
    }

    void Update()
    {
        PlayerControls();
        HandleLookingToAndLookingAwayFromEnemy();
        if(isAttacking)
        {
            attackingModeDurationCtr++;
            if(attackingModeDurationCtr>=80)
            {
                attackingModeDurationCtr = 0;
                isAttacking = false;
            }
        }
        //Debug.Log("Player is attacking "+isAttacking+" ctr: "+attackingModeDurationCtr+"im a fking genius");
    }

    void HandleLookingToAndLookingAwayFromEnemy()
    {
        if(enemyDetected)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentEnemy.transform.position - transform.position), 10 * Time.deltaTime);

            mainCamera.localPosition = new Vector3(0, mainCamera.localPosition.y, mainCamera.localPosition.z);
            if(!currentEnemy.gameObject.activeSelf) //If enemy has died, note: enemy's gameObject=false is defined as enemy=dead for now
            {
                enemyDetected = false;
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*10);

            ConstantForwardMovement();
            //mainCamera.position = new Vector3(-0.8361113f, mainCamera.position.y, mainCamera.position.z);
        }
    }
    void ConstantForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }


    void PlayerControls()
    {
        if(!hittingLeftAreaBlocker)
        {
            if (Input.GetButton (""+KeyCode.A))
            {
                PlayerAnimation.PlayLeftMoveAnimation();
                transform.Translate(Vector3.left * Time.deltaTime);
            }
        }

        if(!hittingRightAreaBlocker)
        {
            if (Input.GetButton (""+KeyCode.D))
            {
                PlayerAnimation.PlayRightMoveAnimation();
                transform.Translate(Vector3.right * Time.deltaTime);
            }
        }

        if (!Input.GetButton (""+KeyCode.A) && !Input.GetButton (""+KeyCode.D))
        {
            PlayerAnimation.PlayIdleAnimation();
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Left Area Blocker")
        {
            hittingLeftAreaBlocker = true;
        }

        if(collision.name == "Right Area Blocker")
        {
            hittingRightAreaBlocker = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.name == "Left Area Blocker")
        {
            hittingLeftAreaBlocker = false;
        }

        if(collision.name == "Right Area Blocker")
        {
            hittingRightAreaBlocker = false;
        }
    }
    
}
