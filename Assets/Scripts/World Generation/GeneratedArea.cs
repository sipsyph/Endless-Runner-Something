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

    // Update is called once per frame
    void Update()
    {
        if(shouldStartCounting)
        {
            counter++; 
            if (counter > 480) //After 480 frames, remove this area and everything on it
            {
                this.transform.parent.transform.parent = GeneratedAreaController.staticGarbageControllerObj.transform;
                this.transform.parent.gameObject.SetActive(false);
                counter = 0;

                shouldStartCounting = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            shouldStartCounting = true; //Start counting
            Debug.Log("Player has exited");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            shouldStartCounting = false;
            Debug.Log("Colliding with Player");
        }
    }
}
