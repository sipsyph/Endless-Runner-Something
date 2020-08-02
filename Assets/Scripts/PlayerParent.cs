using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{

    public static Transform currentEnemy;
    public static bool enemyDetected;

    public Transform mainCamera;
    void Start()
    {
        enemyDetected = false;
    }

    void Update()
    {
        PlayerControls();

        if(enemyDetected) //if enemy is detected
        {
            transform.LookAt(new Vector3(currentEnemy.position.x, transform.position.y, currentEnemy.position.z));
            mainCamera.localPosition = new Vector3(0, mainCamera.localPosition.y, mainCamera.localPosition.z);
            if(!currentEnemy.gameObject.activeSelf)
            {
                enemyDetected = false;
                
            }
        }else{
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
        if (Input.GetButton (""+KeyCode.A)){
            PlayerAnimation.PlayLeftMoveAnimation();
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetButton (""+KeyCode.D)){
            PlayerAnimation.PlayRightMoveAnimation();
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        if (!Input.GetButton (""+KeyCode.A) && !Input.GetButton (""+KeyCode.D))
        {
            PlayerAnimation.PlayIdleAnimation();
        }
        
    }

    
}
