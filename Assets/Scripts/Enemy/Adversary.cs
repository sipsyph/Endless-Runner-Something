using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adversary : MonoBehaviour
{
    public int health;
    int ctr;
    bool hit;
    // Start is called before the first frame update
    void Start()
    {
        ctr = 0;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        ResetColorAfterHit();
        HandleHealthPoints();
    }

    void HandleHealthPoints()
    {
        if(health<=0)
        {
            if(PlayerParent.currentEnemy == this.transform)
            {
                PlayerParent.currentEnemyIsDead = true;
            }
            //transform.gameObject.SetActive(false);
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
            PlayerParent.currentEnemyHealth = health;
        }
    }

    private void OnTriggerExit(Collider collision)
    {

        if(PlayerParent.isAttacking && collision.tag == "Weapon")
        {
            Debug.Log("Adversary is hit by weapon");
            
            hit = true;
            health--;
            PlayerParent.currentEnemyHealth = health;
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }


    }
}
