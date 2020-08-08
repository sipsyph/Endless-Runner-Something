using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedArea : MonoBehaviour
{
    
    private int counter;
    private bool thisAreaIsHit, finishedCounting, shouldStartCounting;
    
    void Start()
    {
        counter = 0;
        thisAreaIsHit = false;
        shouldStartCounting = false;
    }

    void Update()
    {

    }



    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //PlayerParent.isJumping = true;
            Debug.Log("Player has exited");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //PlayerParent.isJumping = false;
            Debug.Log("Staying with Player");
        }
    }
}
