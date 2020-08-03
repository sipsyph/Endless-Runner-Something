using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int ctr;

    void Update()
    {
        //if enemy detects player, look at player
        if(PlayerParent.currentEnemy == this.transform)
        {
            Debug.Log(transform.gameObject.name+" has been detected");
            var newRotation = Quaternion.LookRotation(PlayerParent.playerHeadStatic.position - transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 2 * Time.deltaTime);

        }

    }


    private void OnTriggerEnter(Collider collision)
    {
        // if(collision.tag == "Player")
        // {
        //     Debug.Log("Player detected");
        //     PlayerParent.currentEnemy = collision.transform;
        //     PlayerParent.enemyDetected = true;
        // }

        // if(PlayerParent.isAttacking && collision.tag == "Weapon")
        // {
        //     Debug.Log("Enemy detected");
        //     PlayerParent.currentEnemy = this.transform;
        //     PlayerParent.enemyDetected = true;
        // }
    }

}
