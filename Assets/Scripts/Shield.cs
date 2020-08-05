using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector2 deltaPos;
    private CharacterController controllerPlayer;

    public GameObject shieldModel;

    private bool isHittingBorder, hit;
    private int ctr, changeColorCtr;
    void Start()
    {
        hit = false;
        controllerPlayer = GetComponent<CharacterController>();
    }
    void Update () 
    {
        TouchJoystickControl();
        BandAidFixToShieldGettingOutOfBorders();
        ResetColorAfterHit();
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
            -8.573f);
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
                        -8.573f),
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

    void ResetColorAfterHit()
    {
        if(hit)
        {
            changeColorCtr++;
            if(changeColorCtr >= 20)
            {
                changeColorCtr = 0;
                hit = false;
                //shieldModel.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Projectile")
        {
            hit = true;
            //shieldModel.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }


}
