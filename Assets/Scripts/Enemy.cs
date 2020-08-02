using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    int ctr, health;
    bool hit;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        ctr=0;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        ResetColorAfterHit();
        HandleHealthPoints();
    }

    void HandleHealthPoints()
    {
        if(health<=0)
        {
            transform.gameObject.SetActive(false);
        }
    }
    void ResetColorAfterHit()
    {
        if(hit)
        {
            ctr++;
            if(ctr >= 30)
            {
                ctr = 0;
                hit = false;
                GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Weapon")
        {
            Debug.Log("Enemy is hit by weapon");
            hit = true;
            health--;
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }
}
