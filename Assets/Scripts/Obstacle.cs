using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damageDealtToPlayer;
    void Start()
    {
        damageDealtToPlayer = 1; //Default value for testing, remove in production
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player Hitbox")
        {
            if(this.transform.name.Contains("Jump"))
            {
                Player.playerGotHitByJumpWall = true;
            }
            else if(this.transform.name.Contains("Slide"))
            {
                Player.playerGotHitBySlideWall = true;
            }
            
            Debug.Log("PLAYER HIT OBSTACLE "+this.transform.name);
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player Hitbox")
        {
            Player.playerHealth-=damageDealtToPlayer;
            //Player.playerGotHit = false;
        }
    }
}
