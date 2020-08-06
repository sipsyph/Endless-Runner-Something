using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysDetection : MonoBehaviour
{
    public GameObject bow;
    private bool playerDetected;

    public float movementSpeed, rotationSpeed;

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {  
       if(playerDetected)
       {
            RotateTowardsPlayer();
            if(transform.parent.transform.name.Contains("Melee")) //Ranged enemies should not run after player
            {
                ConstantForwardMovement();
            }       
            
       } 
    }

    void RotateTowardsPlayer()
    {
        var newRotation = Quaternion.LookRotation(PlayerParent.playerHeadStatic.position - transform.parent.transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.parent.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

    }

    void ConstantForwardMovement()
    {
        if(!PlayerParent.enemyDetected)
        {   
            transform.parent.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            
            if(transform.parent.transform.name.Contains("Melee"))
            {
                
            }else{
                PlayerParent.enemyDetected = true;
                bow.SetActive(true);
            }
            //PlayerParent.activatedEnemy = transform.parent.transform;
            PlayerParent.currentEnemyHealth = health;
            PlayerParent.currentEnemy = this.transform.parent.transform;
            
            EnemyAnimation.enemyAnimator = transform.parent.transform.gameObject.GetComponent<Animator>();
            //Debug.Log("Current Enemy animator: "+EnemyAnimation.enemyAnimator.name);
            playerDetected = true;
            Debug.Log(transform.parent.transform.name+" has detected player");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player has left enemy detection");
            if(transform.parent.transform.name.Contains("Melee"))
            {

            }else{
                bow.SetActive(false);
                PlayerParent.projectileIncomingIndicatorStatic.SetActive(false);

            }
            playerDetected = false;
        }
    }
    
}
