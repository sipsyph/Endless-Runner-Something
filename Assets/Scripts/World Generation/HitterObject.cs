using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterObject : MonoBehaviour
{
    public GameObject areaInstancePrefab;

    void Start()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Object To Hit Before Generation")
        {
            Debug.Log("To Hit B4 Generate is HIT! A new area will now be generated in front of player");
            
            //Instantiate(areaInstancePrefab, new Vector3(collision.transform.parent.position.x, 0, collision.transform.parent.position.z +105f), collision.transform.parent.rotation);
            areaInstancePrefab.transform.position = new Vector3(collision.transform.parent.position.x, 0, collision.transform.parent.position.z +105f);

        }
    }
}
