using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform shieldObj;

    public static bool leftHandSelected, rightHandSelected, playerGotHit;

    public static int playerHealth = 1000;
    public static GameObject objectToHitBeforeGeneration;
    int weaponPanelLayerMask;
    Ray ray;
    RaycastHit hit;
    void Start()
    {
        playerHealth = 1000;
        playerGotHit = false;
        weaponPanelLayerMask = LayerMask.GetMask("Weapon Panel");
        rightHandSelected = true;
        leftHandSelected = true;
    }

    void Update()
    {
        //WeaponControls();
        //ToggleWeaponAttackPanels();


    }


    // void WeaponControls()
    // {
    //     if(rightHandSelected)
    //     {
    //         if (Input.GetMouseButtonDown(0))
    //         {
    //             ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //             Debug.Log("Clicked Mouse BUtton");
    //             if (Physics.Raycast(ray, out hit, 2475.0f, weaponPanelLayerMask))
    //             {
    //                 Debug.Log("Hit "+hit.transform.name);
    //                 if (hit.transform == upperLeftPanel)
    //                 {
    //                     Debug.Log("upperLeftPanel Hit");
    //                     SwordAnimation.PlayDownwardSlashLeftToRight();
    //                 }

    //                 if (hit.transform == upperRightPanel)
    //                 {
    //                     Debug.Log("upperRightPanel Hit");
    //                     SwordAnimation.PlayDownwardSlashRightToLeft();
    //                 }

    //                 if (hit.transform == bottomLeftPanel)
    //                 {
    //                     Debug.Log("bottomLeftPanel Hit");
    //                     SwordAnimation.PlayUpwardSlashLeftToRight();
    //                 }

    //                 if (hit.transform == bottomRightPanel)
    //                 {
    //                     Debug.Log("bottomRightPanel Hit");
    //                     SwordAnimation.PlayUpwardSlashRightToLeft();
    //                 }
    //             }
    //         }
    //     }
        
    // }

}
