using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private Vector2 deltaPos;
    private CharacterController controllerPlayer;

    public GameObject shieldModel;

    private bool isHittingBorder, hit;

    public static bool shieldBlocked;
    private int ctr, changeColorCtr;

    private Material originalMaterial;

    public Material changedMaterial;
    void Start()
    {
        originalMaterial = shieldModel.GetComponent<Renderer>().material;
        //changedMaterial = shieldModel.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        shieldBlocked = false;
        hit = false;
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


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Projectile")
        {
            hit = true;
        }

        if(collision.tag == "Enemy Weapon")
        {
            PlayerCollision.playerInvulnerable = true;
            shieldBlocked = true;
            shieldModel.GetComponent<Renderer>().material=changedMaterial;
        }
    }

    private void OnTriggerExit(Collider collision)
    {

        if(collision.tag == "Enemy Weapon")
        {
            shieldBlocked = false;
            shieldModel.GetComponent<Renderer>().material=originalMaterial;
        }
    }


}
