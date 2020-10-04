using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightShieldBlock : MonoBehaviour
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
        Debug.Log ("Triggered R");
        PlayerParent.allowWalkAnimation = false;
        PlayerAnimation.PlayWalkingRightShield();
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        PlayerParent.allowWalkAnimation = true;
        PlayerAnimation.PlayWalkAnimation();
    }
}
