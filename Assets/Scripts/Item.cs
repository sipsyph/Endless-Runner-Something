using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerPickUpThisItem()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag.Equals("Player Hitbox"))
        {
            PlayerPickUpThisItem();
            this.transform.gameObject.SetActive(false);
        }

    }
}
