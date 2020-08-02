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

        Debug.Log("Hitter object colliding with "+collision.name);

        if(collision.tag == "Object To Hit Before Generation")
        {
            Debug.Log("To Hit B4 Generate is HIT! HITDASLADJSALKDASJ");
            
            Instantiate(areaInstancePrefab, new Vector3(collision.transform.parent.position.x, 0, collision.transform.parent.position.z +105f), collision.transform.parent.rotation);
            
        }

        if(collision.tag == "Enemy")
        {
            Debug.Log("Enemy detected");
            PlayerParent.currentEnemy = collision.transform;
            PlayerParent.enemyDetected = true;
        }
    }
}
