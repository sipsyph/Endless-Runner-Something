using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysDetection : MonoBehaviour
{
    public GameObject bow, meleeRangeDetector;
    private bool playerDetected;

    public float movementSpeed, rotationSpeed;

    private Transform enemyTransform;
    private string enemyName;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = transform.parent.transform;
        enemyName = enemyTransform.name;
        movementSpeed = 1f;
        rotationSpeed = 1f;
    }

    void Update()
    {  
       if(playerDetected)
       {
            RotateTowardsPlayer();
            if(enemyName.Contains("Melee"))
            {
                //ConstantForwardMovement();
                meleeRangeDetector.SetActive(true);
            }       
            
       } 
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

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            
            if(enemyName.Contains("Melee"))
            {
                
            }else{
                PlayerParent.enemyDetected = true;
                bow.SetActive(true);
            }
            PlayerParent.currentEnemy = enemyTransform;
            
            EnemyAnimation.enemyAnimator = enemyTransform.gameObject.GetComponent<Animator>();
            //Debug.Log("Current Enemy animator: "+EnemyAnimation.enemyAnimator.name);
            playerDetected = true;
            //Debug.Log(transform.parent.transform.name+" has detected player");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            //When this occurs, it should mean that the player ran away from the enemy
            //without getting locked on to it
            Debug.Log("Player has left enemy detection");
            if(enemyName.Contains("Melee"))
            {
                meleeRangeDetector.SetActive(false);
            }else{
                bow.SetActive(false);
                PlayerParent.projectileIncomingIndicatorStatic.SetActive(false);

            }
            playerDetected = false;
            enemyTransform.gameObject.SetActive(false);
        }
    }
    
}
