using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        PlayerControls();
        ConstantForwardMovement();
    }

    void ConstantForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }


    void PlayerControls()
    {
        if (Input.GetButton (""+KeyCode.A)){
            PlayerAnimation.PlayLeftMoveAnimation();
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetButton (""+KeyCode.D)){
            PlayerAnimation.PlayRightMoveAnimation();
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        if (!Input.GetButton (""+KeyCode.A) && !Input.GetButton (""+KeyCode.D))
        {
            PlayerAnimation.PlayIdleAnimation();
        }
        
    }
}
