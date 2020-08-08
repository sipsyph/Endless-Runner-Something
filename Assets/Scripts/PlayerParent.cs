using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    public Transform mainCamera, playerBody, playerHead;
    public GameObject projectileIncomingIndicator;
    public static Transform currentEnemy, playerBodyStatic, playerHeadStatic, activatedEnemy;
    public static GameObject projectileIncomingIndicatorStatic;
    public static bool enemyDetected, isAttacking, isJumping, isInAttackRange, isSliding;

    public static int currentEnemyHealth, attackingModeDurationCtr;

    public static bool hittingLeftAreaBlocker, hittingRightAreaBlocker;
    public float baseMovementSpeed, jumpSpeedMultiplier, slideSpeedMultiplier, strafingSpeed,speed;
    private int slideCtr, jumpCtr;
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
        speed = actualSpeed;
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
                transform.Translate((Vector3.left  * strafingSpeed) * Time.deltaTime);
            }
        }

        //Moving Right
        if(!hittingRightAreaBlocker)
        {
            if (Input.GetButton (""+KeyCode.D))
            {
                PlayerAnimation.PlayRightMoveAnimation();
                transform.Translate((Vector3.right * strafingSpeed) * Time.deltaTime);
            }
        }

        if (!Input.GetButton (""+KeyCode.A) && !Input.GetButton (""+KeyCode.D))
        {
            PlayerAnimation.PlayIdleAnimation();
        }

        //Jumping
        if(Input.GetButton(""+KeyCode.C)){
            PlayerAnimation.PlayJumpAnimation();
            isJumping = true;
        }
        if(isJumping)
        {
            speed = actualSpeed = baseMovementSpeed * jumpSpeedMultiplier;
            jumpCtr++;
            if(jumpCtr>30)
            {
                if(jumpCtr>6)
                {
                    speed = actualSpeed = baseMovementSpeed;
                }
                jumpCtr = 0;
                isJumping = false;
                
                PlayerAnimation.PlayWalkAnimation();
            }
        }
        else
        {
            jumpCtr = 0;
        }

        //Sliding
        if(Input.GetButton(""+KeyCode.V) && !isJumping){
            PlayerAnimation.PlaySlideAnimation();
            isSliding = true;
        }
        if(isSliding)
        {
            speed = actualSpeed = baseMovementSpeed * slideSpeedMultiplier;
            slideCtr++;
            if(slideCtr>45)
            {
                slideCtr = 0;
                isSliding = false;
                speed = actualSpeed = baseMovementSpeed;
                PlayerAnimation.PlayWalkAnimation();
            }
        }
    }
    

}
