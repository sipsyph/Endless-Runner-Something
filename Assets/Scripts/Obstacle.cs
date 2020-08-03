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
        if(collision.tag == "Player")
        {
            Player.playerGotHit = true;
            Player.playerHealth-=damageDealtToPlayer;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Player.playerGotHit = false;
        }
    }
}
