using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysDetection : MonoBehaviour
{
    public GameObject bow;
    private bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
       if(playerDetected)
       {
            var newRotation = Quaternion.LookRotation(PlayerParent.playerHeadStatic.position - transform.parent.transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.parent.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 2 * Time.deltaTime);

            if(transform.parent.transform.name.Contains("Melee")) //Ranged enemies should not run after player
            {
                ConstantForwardMovement();
            }       
            
       } 
    }

    void ConstantForwardMovement()
    {
        if(!PlayerParent.enemyDetected)
        {   
            transform.parent.transform.Translate(Vector3.forward * Time.deltaTime * 1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Enemy has detected player");
            if(transform.parent.transform.name.Contains("Melee"))
            {

            }else{
                bow.SetActive(true);
            }
            PlayerParent.activatedEnemy = transform.parent.transform;
            EnemyAnimation.enemyAnimator = transform.parent.transform.gameObject.GetComponent<Animator>();
            Debug.Log("Current Enemy animator: "+EnemyAnimation.enemyAnimator.name);
            playerDetected = true;
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
