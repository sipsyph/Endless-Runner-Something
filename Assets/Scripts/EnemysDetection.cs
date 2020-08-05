using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysDetection : MonoBehaviour
{
    public GameObject bow;
    private bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
       if(playerDetected)
       {
            var newRotation = Quaternion.LookRotation(PlayerParent.playerHeadStatic.position - transform.parent.transform.position);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.parent.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 2 * Time.deltaTime);
       } 
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Enemy has detected player");
            bow.SetActive(true);
            playerDetected = true;
        }
    }
}
