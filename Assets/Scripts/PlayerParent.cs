using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    public Transform mainCamera, playerBody, playerHead;
    public GameObject projectileIncomingIndicator;
    public static Transform currentEnemy, playerBodyStatic, playerHeadStatic, activatedEnemy;
    public static GameObject projectileIncomingIndicatorStatic;
    public static bool enemyDetected, isAttacking, isJumping, isInAttackRange;

    public static int currentEnemyHealth, attackingModeDurationCtr;

    private bool isSliding;

    public static bool hittingLeftAreaBlocker, hittingRightAreaBlocker;

    

    public float baseMovementSpeed, jumpSpeedMultiplier, slideSpeedMultiplier;
    private int slideCtr;
    private float actualSpeed;
    void Start()
    {
        slideCtr = 0;
        projectileIncomingIndicatorStatic = projectileIncomingIndicator;
        playerBodyStatic = playerBody;
        playerHeadStatic = playerHead;
        enemyDetected = false;
        hittingLeftAreaBlocker = false;
        hittingRightAreaBlocker = false;
        actualSpeed = baseMovementSpeed;
    }

    void Update()
    {
        PlayerControls();
        HandleLookingToAndLookingAwayFromEnemy();
        HandleAttackModeStates();
        
    }

    void PlayerReaction()
    {

    }

    void HandleAttackModeStates()
    {
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentEnemy.transform.position - transform.position), 5 * Time.deltaTime);
            mainCamera.localPosition = new Vector3(0, mainCamera.localPosition.y, mainCamera.localPosition.z);

            Debug.Log("Player in range "+isInAttackRange);
            if(!isInAttackRange)
            {
                Debug.Log("Player not in range");
                ConstantForwardMovement();
            }

            if(!currentEnemy.gameObject.activeSelf) //If enemy has died, note: enemy's gameObject=false is defined as enemy=dead for now
            {
                projectileIncomingIndicatorStatic.SetActive(false);
                enemyDetected = false;
                isInAttackRange = false;
            }
        }
        else
        {
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*10);

            ConstantForwardMovement();
        }
    }
    void ConstantForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * actualSpeed);
    }

    void PlayerControls()
    {
        //Moving Left
        if(!hittingLeftAreaBlocker)
        {
            if (Input.GetButton (""+KeyCode.A))
            {
                PlayerAnimation.PlayLeftMoveAnimation();
                transform.Translate(Vector3.left * Time.deltaTime);
                actualSpeed = baseMovementSpeed;
            }
        }

        //Moving Right
        if(!hittingRightAreaBlocker)
        {
            if (Input.GetButton (""+KeyCode.D))
            {
                PlayerAnimation.PlayRightMoveAnimation();
                transform.Translate(Vector3.right * Time.deltaTime);
                actualSpeed = baseMovementSpeed;
            }
        }

        if (!Input.GetButton (""+KeyCode.A) && !Input.GetButton (""+KeyCode.D))
        {
            PlayerAnimation.PlayIdleAnimation();
            actualSpeed = baseMovementSpeed;
        }

        //Jumping
        if(Input.GetButton(""+KeyCode.C) && !isJumping){
            PlayerAnimation.PlayJumpAnimation();
        }
        if(isJumping)
        {
            actualSpeed = baseMovementSpeed * jumpSpeedMultiplier; 
        }

        //Sliding
        if(Input.GetButton(""+KeyCode.V) && !isJumping){
            PlayerAnimation.PlaySlideAnimation();
            isSliding = true;
        }
        if(isSliding)
        {
            actualSpeed = baseMovementSpeed * slideSpeedMultiplier;
            slideCtr++;
            if(slideCtr>80)
            {
                slideCtr = 0;
                isSliding = false;
            }
        }
    }
    

}
