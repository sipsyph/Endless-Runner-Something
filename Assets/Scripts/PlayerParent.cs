using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerParent : MonoBehaviour
{
    public Button leftBtn, rightBtn; //Temporary buttons for debugging
    
    public Transform mainCamera, secondCamera, playerBody, playerHead, playerInteractableWeapons, playerModel;
    public GameObject projectileIncomingIndicator;
    public static Transform currentEnemy, playerBodyStatic, playerHeadStatic, activatedEnemy;
    public static GameObject projectileIncomingIndicatorStatic;
    public static bool playerLookingInBag, enemyDetected, isAttacking, isJumping, isInAttackRange, isSliding, currentEnemyIsDead;

    public static int currentEnemyHealth, attackingModeDurationCtr;

    public static bool hittingLeftAreaBlocker, hittingRightAreaBlocker;
    public float baseMovementSpeed, jumpSpeedMultiplier, slideSpeedMultiplier, strafingSpeed,speed, playerModelSlidingXOffset;
    private int slideCtr, jumpCtr;
    private float actualSpeed;

    private bool leftBtnPressing, rightBtnPressing;
    void Start()
    {
        currentEnemyIsDead = false;
        playerLookingInBag = false;
        slideCtr = 0;
        projectileIncomingIndicatorStatic = projectileIncomingIndicator;
        playerBodyStatic = playerBody;
        playerHeadStatic = playerHead;
        enemyDetected = false;
        hittingLeftAreaBlocker = false;
        hittingRightAreaBlocker = false;
        actualSpeed = baseMovementSpeed;
        speed = actualSpeed;

        leftBtn.onClick.AddListener(() =>
        {
            if(leftBtnPressing)
            {
                leftBtnPressing = false;
            }else{
                leftBtnPressing = true;
            }
            rightBtnPressing = false;
        });

        rightBtn.onClick.AddListener(() =>
        {
            if(rightBtnPressing)
            {
                rightBtnPressing = false;
            }else{
                rightBtnPressing = true;
            }
            leftBtnPressing = false;
        });

    }

    void Update()
    {
        PlayerControls();
        HandleLookingToAndLookingAwayFromEnemy();
        HandleAttackModeStates();
    }

    void CameraFightingMode()
    {
        //TODO: smooth transition from this mode to the other
        playerModel.gameObject.SetActive(false);
        mainCamera.GetComponent<Camera>().enabled = true;
        secondCamera.GetComponent<Camera>().enabled = false;
        playerInteractableWeapons.gameObject.SetActive(true);
    }

    void CameraNotFightingMode()
    {
        //TODO: smooth transition from this mode to the other
        playerModel.gameObject.SetActive(true);
        mainCamera.GetComponent<Camera>().enabled = false;
        secondCamera.GetComponent<Camera>().enabled = true;
        playerInteractableWeapons.gameObject.SetActive(false);
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
            CameraFightingMode();
            var newRotation = Quaternion.LookRotation(currentEnemy.transform.position - transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 5f * Time.deltaTime);

            //mainCamera.localPosition = new Vector3(0, mainCamera.localPosition.y, mainCamera.localPosition.z);

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
            CameraNotFightingMode();
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
            if (Input.GetButton (""+KeyCode.A) || leftBtnPressing)
            {
                PlayerAnimation.PlayLeftMoveAnimation();
                transform.Translate((Vector3.left  * strafingSpeed) * Time.deltaTime);
                BandageFixToPlayerModelSlidingXOvershoot(-10f);
            }
        }

        //Moving Right
        if(!hittingRightAreaBlocker)
        {
            if (Input.GetButton (""+KeyCode.D) || rightBtnPressing)
            {
                PlayerAnimation.PlayRightMoveAnimation();
                transform.Translate((Vector3.right * strafingSpeed) * Time.deltaTime);
                BandageFixToPlayerModelSlidingXOvershoot(10f);
            }
        }

        if (!Input.GetButton (""+KeyCode.A) && !Input.GetButton (""+KeyCode.D) && !playerLookingInBag)
        {
            PlayerAnimation.PlayIdleAnimation();
            BandageFixToPlayerModelSlidingXOvershoot(0);

        }

        //Jumping
        if( (Input.GetButtonDown(""+KeyCode.C) || DraggingOnCanvas.draggedUp) && !isJumping )
        {
            Debug.Log("Jump Button");
            slideCtr = 0;
            isSliding = false;
            PlayerAnimation.PlayWalkAnimation();
            PlayerAnimation.PlayJumpAnimation();
            isJumping = true;
            DraggingOnCanvas.draggedUp = false;
        }
        if(isJumping)
        {
            speed = actualSpeed = baseMovementSpeed * jumpSpeedMultiplier;
        }

        //Sliding
        if( (Input.GetButtonDown(""+KeyCode.V) || DraggingOnCanvas.draggedDown) && !isSliding ){
            Debug.Log("Slide Button");
            isJumping = false;
            PlayerAnimation.PlayWalkAnimation();
            PlayerAnimation.PlaySlideAnimation();
            slideCtr = 0;
            isSliding = true;
            DraggingOnCanvas.draggedDown = false;
        }
        if(isSliding)
        {
            speed = actualSpeed = baseMovementSpeed * slideSpeedMultiplier;
            slideCtr++;
            if(slideCtr>=45)
            {
                isSliding = false;
                slideCtr = 0;
            }
        }

        if(!isSliding && !isJumping)
        {
            PlayerAnimation.PlayWalkAnimation();
            speed = actualSpeed = baseMovementSpeed;
        }
    }

    public void BandageFixToPlayerModelSlidingXOvershoot(float yValue)
    {
        if(isSliding)
        {
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, Quaternion.Euler(playerModelSlidingXOffset,0,0), 5f * Time.deltaTime);

        }else{
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, Quaternion.Euler(0f,yValue,0), 5f * Time.deltaTime);
        }
    }
    

}
