using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool playerInvulnerable;
    private int ctr;
    void Start()
    {
        ctr=0;
        playerInvulnerable = false;
    }

    void Update()
    {
        HandleInvulnerability();
    }

    void HandleInvulnerability()
    {
        if(playerInvulnerable)
        {
            Debug.Log("Player is invulnerable");
            ctr++;
            if(ctr>=30)
            {
                playerInvulnerable = false;
                ctr=0;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Left Area Blocker")
        {
            PlayerParent.hittingLeftAreaBlocker = true;
        }

        if(collision.name == "Right Area Blocker")
        {
            PlayerParent.hittingRightAreaBlocker = true;
        }

        if(collision.tag == "Projectile")
        {
            Debug.Log("Hit by arrow");
            Player.playerHealth--;
            Player.playerGotHit = true;
        }

        if(collision.tag == "Enemy Weapon" && !playerInvulnerable)
        {
            Debug.Log("Hit by weapon");
            Player.playerHealth--;
            Player.playerGotHit = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.name.Contains("Cylinder"))
        {
            PlayerParent.climbingOnVine = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.name == "Left Area Blocker")
        {
            PlayerParent.hittingLeftAreaBlocker = false;
        }

        if(collision.name == "Right Area Blocker")
        {
            PlayerParent.hittingRightAreaBlocker = false;
        }

        if(collision.name.Contains("Cylinder"))
        {
            PlayerParent.climbingOnVine = false;
        }
    }
}
