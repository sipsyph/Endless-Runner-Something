using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftShieldBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log ("Triggered L");
        PlayerParent.allowWalkAnimation = false;
        PlayerAnimation.PlayWalkingLeftShield();
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        PlayerParent.allowWalkAnimation = true;
        PlayerAnimation.PlayWalkAnimation();
    }
}
