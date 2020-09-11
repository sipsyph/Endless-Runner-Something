using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerParent : MonoBehaviour
{
    public Button leftBtn, rightBtn; //Temporary buttons for debugging
    
    public Transform mainCamera, secondCamera, playerBody, playerHead, playerInteractableWeapons, playerModel;
    public GameObject projectileIncomingIndicator, backShield, fallingBranchPrefab, eventSystemObj;
    public static Transform currentEnemy, playerBodyStatic, playerHeadStatic, activatedEnemy;
    public static GameObject projectileIncomingIndicatorStatic;
    public static bool playerLookingInBag, enemyDetected, isAttacking, isJumping, isInAttackRange, isSliding, 
    currentEnemyIsDead, climbingOnVine;

    public static int currentEnemyHealth, attackingModeDurationCtr;

    public static bool hittingLeftAreaBlocker, hittingRightAreaBlocker, playerClimbing;
    public float baseMovementSpeed, jumpSpeedMultiplier, slideSpeedMultiplier, strafingSpeed,speed, playerModelSlidingXOffset
    , climbingSpeedMultiplier, climbingJumpSpeedMultiplier, climbingVineSpeedMultiplier;
    private int slideCtr, jumpCtr, fallingBranchCtr;
    private float actualSpeed;

    private bool leftBtnPressing, rightBtnPressing;
    void Start()
    {
        playerClimbing = false;
        fallingBranchCtr = 0;

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

        if(playerClimbing)
        {
            PlayerClimbingMode();
        }else{
            backShield.SetActive(false);
            secondCamera.GetComponent<Camera>().fieldOfView = 63f; //63 is the default value
            HandleLookingToAndLookingAwayFromEnemy();
            HandleAttackModeStates();
        }

    }

    void CameraFightingMode()
    {
        //TODO: smooth transition from this mode to the other
        eventSystemObj.GetComponent<CanvasUI>().weaponBtnGroup.SetActive(true);
        eventSystemObj.GetComponent<CanvasUI>().shieldJoystick.SetActive(true);
        playerModel.gameObject.SetActive(false);
        mainCamera.GetComponent<Camera>().enabled = true;
        secondCamera.GetComponent<Camera>().enabled = false;
        playerInteractableWeapons.gameObject.SetActive(true);
    }

    void CameraNotFightingMode()
    {
        //TODO: smooth transition from this mode to the other
        eventSystemObj.GetComponent<CanvasUI>().weaponBtnGroup.SetActive(false);
        eventSystemObj.GetComponent<CanvasUI>().shieldJoystick.SetActive(false);
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

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 2f * Time.deltaTime);

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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*5);
            ConstantForwardMovement();
        }
    }
    void ConstantForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * actualSpeed);
    }

    void PlayerClimbingMode()
    {
        backShield.SetActive(true);
        secondCamera.GetComponent<Camera>().fieldOfView = 85f;
        CameraNotFightingMode();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*5);
        ConstantForwardMovement();
        SpawnFallingBranchRepeat();
    }

    void SpawnFallingBranchRepeat()
    {
        fallingBranchCtr++;
        if(fallingBranchCtr >= 120)
        {
            fallingBranchPrefab.transform.GetComponent<Animator>().speed = 0.5f;
            Instantiate(fallingBranchPrefab, this.transform);
            fallingBranchCtr = 0;
        }
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
        if(  ((Input.GetButtonDown(""+KeyCode.C) || DraggingOnCanvas.draggedUp) && !isJumping) )
        {
            Debug.Log("Jump Button");
            slideCtr = 0;
            isSliding = false;
            if(playerClimbing)
            {
                PlayerAnimation.PlayClimbAnimation();
                PlayerAnimation.PlayClimbJumpAnimation();
            }else{
                PlayerAnimation.PlayWalkAnimation();
                PlayerAnimation.PlayJumpAnimation();
            }

            isJumping = true;
            DraggingOnCanvas.draggedUp = false;
        }
        if(isJumping)
        {
            if(playerClimbing)
            {
                strafingSpeed = 8f;
                speed = actualSpeed = baseMovementSpeed * climbingJumpSpeedMultiplier;
            }else{
                speed = actualSpeed = baseMovementSpeed * jumpSpeedMultiplier;
            }
            
        }

        //Sliding
        if( !playerClimbing && ((Input.GetButtonDown(""+KeyCode.V) || DraggingOnCanvas.draggedDown) && !isSliding) ){
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

        //Basic Forward Movement
        if(!isSliding && !isJumping)
        {
            if(playerClimbing)
            {
                
                
                if(climbingOnVine)
                {
                    actualSpeed = baseMovementSpeed * climbingVineSpeedMultiplier;
                    strafingSpeed =4f;
                }else{
                    actualSpeed = baseMovementSpeed * climbingSpeedMultiplier;
                    strafingSpeed = 1.5f;
                }
                PlayerAnimation.PlayClimbAnimation();
            }else{
                strafingSpeed = 3f;
                speed = actualSpeed = baseMovementSpeed;
                PlayerAnimation.PlayWalkAnimation();
            }
            
            
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
