using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adversary : MonoBehaviour
{
    public int health;
    private int maxHealth;
    int ctr;
    bool hit;
    void Start()
    {
        ctr = 0;
        hit = false;
        maxHealth = health;
    }

    void Update()
    {
        ResetColorAfterHit();
        HandleHealthPoints();
    }

    void HandleHealthPoints()
    {
        if(!PlayerParent.currentEnemyIsDead && health<=0)
        {
            if(PlayerParent.currentEnemy == this.transform)
            {
                PlayerParent.currentEnemyIsDead = true;
                health = maxHealth; //reset health for next spawn
            }
        }
    }
    void ResetColorAfterHit()
    {
        if(hit)
        {
            ctr++;
            if(ctr >= 30)
            {
                ctr = 0;
                hit = false;
                GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Adversary is colliding with tag Player");
            if(transform==PlayerParent.currentEnemy)
            {
                PlayerParent.currentEnemyHealth = health;
            }
            
        }

        if(PlayerParent.isAttacking && collision.tag == "Weapon")
        {
            Debug.Log("Adversary is hit by weapon");
            
            hit = true;
            health--;
            if(transform==PlayerParent.currentEnemy)
            {
                PlayerParent.currentEnemyHealth = health;
            }
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

    private void OnTriggerExit(Collider collision)
    {




    }
}
