using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    int ctr;
    bool hit;
    // Start is called before the first frame update
    void Start()
    {
        ctr=0;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        ResetColorAfterHit();
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
                EnemyAnimation.PlayIdleAnimation();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        hit = true;
        //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        EnemyAnimation.PlayGettingHitAnimation();
        Debug.Log("Enemy hit by "+collision.transform.name);
    }
}
