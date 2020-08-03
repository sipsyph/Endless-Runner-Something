using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector2 deltaPos;
    private CharacterController controllerPlayer;

    private bool isHittingBorder;
    private int ctr;
    void Start()
    {
        controllerPlayer = GetComponent<CharacterController>();
    }
    void Update () 
    {
        TouchJoystickControl();
        BandAidFixToShieldGettingOutOfBorders();
    }

    void TouchJoystickControl()
    {
        Vector3 mov = new Vector3(
        SimpleInput.GetAxis("Horizontal")*10f,
        SimpleInput.GetAxis("Vertical")*10f,
        0);
    
        controllerPlayer.Move(mov*Time.deltaTime);
        transform.localPosition = new Vector3 (
            this.transform.localPosition.x, 
            this.transform.localPosition.y, 
            -8.35f);
    }

    void BandAidFixToShieldGettingOutOfBorders()
    {
        if(PlayerParent.enemyDetected)
        {
            ctr++;
            if(ctr<=30)
            {
                transform.localPosition = Vector3.MoveTowards(
                    transform.localPosition, new Vector3(
                        -0.9439999f,
                        0.993f,
                        -8.44f),
                    20f * Time.deltaTime);
            }
            else
            {
                ctr = 31;
            }
        }
        else
        {
            ctr=0;
        }
    }


}
