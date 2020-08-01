using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector2 deltaPos;
    private CharacterController controllerPlayer;
    void Start()
    {
        controllerPlayer = GetComponent<CharacterController>();
    }
    void Update () 
    {
        //FollowTouch();
        // Vector3 mov = new Vector3(
        //     SimpleInput.GetAxis("Horizontal")*10f,
        //     SimpleInput.GetAxis("Vertical")*10f,
        //     0);

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

    void FollowTouch()
    {
        if(Player.leftHandSelected)
        {
            Vector2 touchPos;
            if (Input.GetMouseButtonDown (0)) 
            {                                                                  
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

                if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {

                    touchPos = new Vector3 (hit.point.x, hit.point.y);
                    deltaPos.x = touchPos.x - transform.position.x;
                    deltaPos.y = touchPos.y - transform.position.y;
                }
            }
            if (Input.GetMouseButton (0)) 
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

                if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
                    transform.localPosition = new Vector3 (hit.point.x - deltaPos.x, hit.point.y - deltaPos.y, -8.5f);  //-8.5f
                }
            }
        }
        else
        {
            transform.localPosition = new Vector3 (-0.9439999f, 0.993f, -8.44f);
        }
    }
}
