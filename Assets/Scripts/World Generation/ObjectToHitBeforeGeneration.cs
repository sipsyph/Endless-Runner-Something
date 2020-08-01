using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHitBeforeGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //When this object is created, make THIS object the current obj to hit
        Player.objectToHitBeforeGeneration = this.transform.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            this.transform.gameObject.SetActive(false);
        }
    }
}
