using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform upperLeftPanel, upperRightPanel, bottomLeftPanel, bottomRightPanel; 
    int weaponPanelLayerMask;
    Ray ray;
    RaycastHit hit;
    void Start()
    {
        weaponPanelLayerMask = LayerMask.GetMask("Weapon Panel");
    }

    // Update is called once per frame
    void Update()
    {
        //ConstantForwardMovement();
        PlayerControls();
        WeaponControls();
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

    void WeaponControls()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Clicked Mouse BUtton");
            if (Physics.Raycast(ray, out hit, 2475.0f, weaponPanelLayerMask))
            {
                Debug.Log("Hit "+hit.transform.name);
                if (hit.transform == upperLeftPanel)
                {
                    Debug.Log("upperLeftPanel Hit");
                    SwordAnimation.PlayDownwardSlashLeftToRight();
                }

                if (hit.transform == upperRightPanel)
                {
                    Debug.Log("upperRightPanel Hit");
                    SwordAnimation.PlayDownwardSlashRightToLeft();
                }

                if (hit.transform == bottomLeftPanel)
                {
                    Debug.Log("bottomLeftPanel Hit");
                    SwordAnimation.PlayUpwardSlashLeftToRight();
                }

                if (hit.transform == bottomRightPanel)
                {
                    Debug.Log("bottomRightPanel Hit");
                    SwordAnimation.PlayUpwardSlashRightToLeft();
                }
            }
        }else{
            //SwordAnimation.PlayIdleAnimation();
        }
    }
}
